using System.Collections.Generic;
using System.Threading.Tasks;
using eCommerce.Bussiness.TokenService;
using eCommerce.Entitys.Response;
using eCommerce.Entitys.Tokens;
using eCommerce.Entitys;
using eCommerce.Data.IRepository;

namespace eCommerce.Bussiness.AuthenticationService
{
    public class Authenticator
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public Authenticator(TokenGenerator tokenGenerator, RefreshTokenGenerator refreshTokenGenerator, IRefreshTokenRepository refreshTokenRepository)
        {
            _tokenGenerator = tokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticationResponse> Authentication(Cliente existingUser)
        {
            string token = null;
            string refreshToken = null;

                token = _tokenGenerator.GenerateToken(existingUser);
                refreshToken = _refreshTokenGenerator.GenerateToken();
                RefreshToken refreshClienteToken = new RefreshToken()
                {
                    token = refreshToken,
                    idSesion = existingUser.id,
                    role = $"Cliente"
                };
                await _refreshTokenRepository.CreateRefreshToken(refreshClienteToken);
            
            return new AuthenticationResponse()
            {
                token = token,
                refreshToken = refreshToken
            };

        }
        public async Task<AuthenticationResponse> Authentication(Admin existingUser)
        {
            string token = null;
            string refreshToken = null;

            token = _tokenGenerator.GenerateToken(existingUser);
            refreshToken = _refreshTokenGenerator.GenerateToken();
            RefreshToken refreshClienteToken = new RefreshToken()
            {
                token = refreshToken,
                idSesion = existingUser.id,
                role = $"Admin"
            };
            await _refreshTokenRepository.CreateRefreshToken(refreshClienteToken);

            return new AuthenticationResponse()
            {
                token = token,
                refreshToken = refreshToken
            };

        }
    }
}
