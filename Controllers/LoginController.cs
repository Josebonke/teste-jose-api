using Microsoft.AspNetCore.Mvc;
using static teste_jose_api.Model.LoginModel;

namespace teste_jose_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Logar(UsuarioFilter usuario)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                // Tratar erros de lógica de login
                return StatusCode(500, ex.Message);
            }
        }
    }
}