
using AuthService.Domain.Models;
namespace AuthService.Domain.Services
{
    public interface ITokenService
    {
        public Task<IResult<User>> ValidateAccessToken(string accessToken);
        public Task<IResult<string>> GenerateRefreshToken(User user);
        public Task<IResult<string>> GenerateAccessToken(string refreshToken);

    }
}
