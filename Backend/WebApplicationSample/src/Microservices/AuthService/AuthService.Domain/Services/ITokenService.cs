using AuthService.Application.Services;
using AuthService.Domain.Models;
namespace AuthService.Domain.Services
{
    public interface ITokenService
    {
        public Task<Result<string>> GenerateRefreshToken(User user);
        public Task<Result<string>> GenerateAccessToken(string refreshToken);

    }
}
