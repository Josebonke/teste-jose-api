using Microsoft.AspNetCore.Identity;
using teste_jose_api.identity;

namespace teste_jose_api.Dao
{
   
    public interface IUsuarioDAO
    {
        Task<AppUsuario> GetUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(AppUsuario user, string password);
        Task UpdateUserAsync(AppUsuario user);
        Task SetLockoutEndDateAsync(AppUsuario user, DateTimeOffset lockoutEnd);
    }

    public class LoginDao : IUsuarioDAO
    {
        private readonly UserManager<AppUsuario> _userManager;

        public LoginDao(UserManager<AppUsuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUsuario> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckPasswordAsync(AppUsuario user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task UpdateUserAsync(AppUsuario user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task SetLockoutEndDateAsync(AppUsuario user, DateTimeOffset lockoutEnd)
        {
            await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
        }
    }
}

