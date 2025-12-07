
namespace AuthService.Domain.Errors
{
    public enum ErrorCode
    {
        NoExceptions,
        RefreshTokenInvalid,
        NoUserFound,
        AccessTokenInvalid,
        UserNameExists,
        UserNameInvalid,
        PasswordInvalid,
    }
}
