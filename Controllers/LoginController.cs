using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using teste_jose_api.identity;
using teste_jose_api.Model;
using static teste_jose_api.Model.LoginModel;

namespace teste_jose_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly UserManager<AppUsuario> _userManager;
        private readonly IConfiguration _configuration;

        // Construtor com injeção de dependência
        public LoginController(UserManager<AppUsuario> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UsuarioFilter model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Senha))
            {
                if (user != null)
                {
                    user.TentativasDeLoginErradas += 1;

                    if (user.TentativasDeLoginErradas >= 5)
                    {
                        user.Bloqueado = true;
                        await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(15));
                    }

                    await _userManager.UpdateAsync(user);
                }
                return Unauthorized("Login ou senha inválidos.");
            }

            
            user.TentativasDeLoginErradas = 0;
            await _userManager.UpdateAsync(user);

            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, value: user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { Token = tokenHandler.WriteToken(token) });
        }

    }

    

}