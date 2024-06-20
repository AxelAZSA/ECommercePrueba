using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using eCommerce.Entitys.Tokens;
using System.Linq;
using eCommerce.Data.IRepository;

namespace eCommerce.Data.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DbEContext _context;
        public RefreshTokenRepository(DbEContext context)
        {
            _context = context;
        }

        public async Task CreateRefreshToken(RefreshToken refresh)
        {
            await _context.refreshTokens.AddAsync(refresh);
            await Save();
        }

        public async Task DeleteRefreshToken(int tokenId)
        {
            var token = await _context.refreshTokens.FindAsync(tokenId);
            _context.Entry(token).State = EntityState.Deleted;
            try
            {
                await Save();
            }
            catch
            {
                throw;
            }
        }

        public async Task<RefreshToken> GetByToken(string token, string role)
        {
            var tokenDTO = await _context.refreshTokens.FirstOrDefaultAsync(t => t.token == token && t.role == role);

            return tokenDTO;
        }

        public async Task DeleteAll(int UserId, string role)
        {
            var tokens = await _context.refreshTokens.Where(u => u.idSesion == UserId && u.role == role).ToListAsync();
            foreach (var token in tokens)
            {
                await DeleteRefreshToken(token.id);
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
