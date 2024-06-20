using System.Threading.Tasks;
using System;
using eCommerce.Entitys.Tokens;

namespace eCommerce.Data.IRepository
{
    public interface IRefreshTokenRepository
    {
        Task CreateRefreshToken(RefreshToken refresh);
        Task<RefreshToken> GetByToken(string token, string role);
        Task DeleteRefreshToken(int tokenId);
        Task DeleteAll(int UserId, string role);

    }
}
