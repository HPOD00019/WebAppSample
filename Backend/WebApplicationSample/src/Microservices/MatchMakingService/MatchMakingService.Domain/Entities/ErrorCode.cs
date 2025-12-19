
namespace MatchMakingService.Domain.Entities
{
    public enum ErrorCode
    {
        NoExceptions,
        RefreshTokenInvalid,
        UserNotFoundInMatchRequests,
        MatchIsNotFound,
        UserNotFoundInDataBase,
        UpdateTTLtimeControlWasInvalid,
    }
}
