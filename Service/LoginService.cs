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

            var userFindByEmail = await _userRepository.GetUserByEmailAsync(filter.Email);
            var userFindedByPassword = await _userRepository.CheckPasswordAsync(userFindByEmail, filter.Senha);

            if (userFindByEmail.LockoutEnabled)
            {
                await _userRepository.SetLockoutEndDateAsync(userFindByEmail, DateTimeOffset.UtcNow.AddMinutes(15));
                throw new UnauthorizedAccessException("Usuario Bloquado");
            }

            if (userFindByEmail.LockoutEnd <= DateTime.Now)
            {
                userFindByEmail.Bloqueado = false;
                userFindByEmail.LockoutEnabled = true;
                userFindByEmail.TentativasDeLoginErradas = 0;
                await _userRepository.UpdateUserAsync(userFindByEmail);
            }
            {
                await _userRepository.SetLockoutEndDateAsync(userFindByEmail, DateTimeOffset.UtcNow.AddMinutes(15));
                throw new UnauthorizedAccessException("Usuario Bloquado");
            }
            if (userFindByEmail != null && userFindedByPassword)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userFindByEmail.Id),
                        new Claim(ClaimTypes.Email, userFindByEmail.Email)
                                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

            }
            else if(userFindByEmail != null && !userFindedByPassword)
            {
                userFindByEmail.TentativasDeLoginErradas += 1;
                await _userRepository.UpdateUserAsync(userFindByEmail);
                if (userFindByEmail.TentativasDeLoginErradas >= 5)
                {
                    userFindByEmail.Bloqueado = true;
                    await _userRepository.SetLockoutEndDateAsync(userFindByEmail, DateTimeOffset.UtcNow.AddMinutes(15));
                    throw new UnauthorizedAccessException("Usuario Bloquado");
                }
                else
                {
                    throw new UnauthorizedAccessException("Login ou senha inválidos.");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Login ou senha inválidos.");

            }
            
           

            
        }
    }
}

    



