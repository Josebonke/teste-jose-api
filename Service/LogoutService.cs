using teste_jose_api.Dao;
using teste_jose_api.Model;

namespace teste_jose_api.Service
{
    public interface ILogoutTokenService
    {
        Task AddRevokedTokenAsync(string token);
        Task<List<LogoutRequest>> GetAllRevokedTokensAsync();
        Task RemoveRevokedTokenAsync(int id);
    }
    public class LogoutService : ILogoutTokenService
    {
        private readonly ILogoutTokenDao _revokedTokenDao;

        public LogoutService(ILogoutTokenDao revokedTokenDao)
        {
            _revokedTokenDao = revokedTokenDao;
        }

        public async Task AddRevokedTokenAsync(string token)
        {
            var revokedToken = new LogoutRequest
            {
                Token = token,
                RevokedAt = DateTime.UtcNow
            };
            await _revokedTokenDao.AddRevokedTokenAsync(revokedToken);
        }

        public async Task<List<LogoutRequest>> GetAllRevokedTokensAsync()
        {
            return await _revokedTokenDao.GetRevokedTokensAsync();
        }

        public async Task RemoveRevokedTokenAsync(int id)
        {
            await _revokedTokenDao.RemoveRevokedTokenAsync(id);
        }
    }
}
