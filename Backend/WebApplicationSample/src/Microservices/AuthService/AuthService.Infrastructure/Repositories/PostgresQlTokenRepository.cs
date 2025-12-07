
using AuthService.Application.Services;
using AuthService.Domain.Errors;
using AuthService.Domain.Models;
using AuthService.Domain.Repositories;
using AuthService.Domain.Services;
using AuthService.Infrastructure.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories
{
    class PostgresQlTokenRepository : ITokenRepository
    {
        private AuthMicroserviceDbContext _context;

        public PostgresQlTokenRepository(AuthMicroserviceDbContext context)
        {
            _context = context;
        }
        public async Task<RefreshToken> RegisterRefreshToken(RefreshToken token)
        {
            var _token = new RefreshToken(token);
            if (_token.Id == null)
            {
                var maxId = await _context.RefreshTokens.AsNoTracking().MaxAsync(t => (int?)t.Id) ?? 0;

                _token.Id = maxId + 1;
            }
            
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();

            return _token;
        }

        public async Task<List<RefreshToken>> GetRefreshTokensByUserId(int Id)
        {
            var tokens =  _context.RefreshTokens.AsNoTracking().Where(t => t.User ==  Id).ToList<RefreshToken>();
            return tokens;
        }

        public async Task<IResult<RefreshToken>> ValidateRefreshToken(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new Exception("Token value was null in Token Repository");

            var token = _context.RefreshTokens.AsNoTracking().Where(t => t.Value == value).ToList<RefreshToken>();
            if(token.Count > 0)
            {
                var data = token[0]; 
                var result = Result<RefreshToken>.OnSuccess(data);
                return result;
            }
            else
            {
                var error = new Error(ErrorCode.RefreshTokenInvalid, $"No token with value {value}");
                var result = Result<RefreshToken>.OnFailure(error);
                return result;
            }

        }
    }
}
