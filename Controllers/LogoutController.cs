using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using teste_jose_api.Service; 
using teste_jose_api.identity;
using teste_jose_api.Model;

[ApiController]
[Route("[controller]")]
public class LogoutController : ControllerBase
{
    private readonly ILogoutTokenService _revokedTokenService;

    public LogoutController(ILogoutTokenService revokedTokenService)
    {
        _revokedTokenService = revokedTokenService;
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Token))
        {
            return BadRequest(new { message = "Token inválido." });
        }

        await _revokedTokenService.AddRevokedTokenAsync(request.Token);
        return Ok(new { message = "Token revogado com sucesso." });
    }



}
