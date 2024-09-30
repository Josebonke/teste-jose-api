using Microsoft.EntityFrameworkCore;
using teste_jose_api.Model;

namespace teste_jose_api.Dao
{
    public interface ILogoutTokenDao
    {
        Task AddRevokedTokenAsync(LogoutRequest revokedToken);
        Task<List<LogoutRequest>> GetRevokedTokensAsync();
        Task<LogoutRequest> GetRevokedTokenByIdAsync(int id);
        Task RemoveRevokedTokenAsync(int id);
    }
    public class LogoutDao : ILogoutTokenDao
    {
        private readonly ApplicationDbContext _context;

        public LogoutDao(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRevokedTokenAsync(LogoutRequest revokedToken)
        {
            await _context.RevokedTokens.AddAsync(revokedToken);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LogoutRequest>> GetRevokedTokensAsync()
        {
            return await _context.RevokedTokens.ToListAsync();
        }

        public async Task<LogoutRequest> GetRevokedTokenByIdAsync(int id)
        {
            return await _context.RevokedTokens.FindAsync(id);
        }

        public async Task RemoveRevokedTokenAsync(int id)
        {
            var revokedToken = await GetRevokedTokenByIdAsync(id);
            if (revokedToken != null)
            {
                _context.RevokedTokens.Remove(revokedToken);
                await _context.SaveChangesAsync();
            }
        }
    }
}
