using AuthService.Domain.Models;
using AuthService.Domain.Repositories;
using AuthService.Infrastructure.DataBaseContext;

namespace AuthService.Infrastructure.Repositories
{
    class TokenRepository : ITokenRepository
    {
        public void RegisterRefreshToken(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
        }

        private AuthMicroserviceDbContext _context;

        public TokenRepository(AuthMicroserviceDbContext context)
        {
            _context = context;
        }

    }
}
