using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using teste_jose_api.identity;
using teste_jose_api.Model;
using teste_jose_api.Service;
using static teste_jose_api.Model.LoginModel;

namespace teste_jose_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly UserManager<AppUsuario> _userManager;
        private readonly IConfiguration _configuration; 
        private readonly ILoginService _loginService;


        public LoginController(UserManager<AppUsuario> userManager, IConfiguration configuration,ILoginService loginService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UsuarioFilter filter)
        {
            try
            {
                var token = await _loginService.LoginAsync(filter);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }

}

    

