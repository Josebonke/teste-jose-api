using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using teste_jose_api.Dao;
using static teste_jose_api.Model.LoginModel;

namespace teste_jose_api.Service
{
    public interface ILoginService
    {
        Task<string> LoginAsync(UsuarioFilter filter);
    }
    public class LoginService : ILoginService
    {
        private readonly IUsuarioDAO _userRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IUsuarioDAO userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(UsuarioFilter filter)
        {

            var user = await _userRepository.GetUserByEmailAsync(filter.Email);

            if (user == null || !await _userRepository.CheckPasswordAsync(user, filter.Senha))
            {
                if (user != null)
                {
                    user.TentativasDeLoginErradas++;
                    if (user.TentativasDeLoginErradas >= 5)
                    {
                        user.Bloqueado = true;
                        await _userRepository.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(15));
                    }

                    await _userRepository.UpdateUserAsync(user);
                }
                throw new UnauthorizedAccessException("Login ou senha inválidos.");
            }

            user.TentativasDeLoginErradas = 0;
            await _userRepository.UpdateUserAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

    



