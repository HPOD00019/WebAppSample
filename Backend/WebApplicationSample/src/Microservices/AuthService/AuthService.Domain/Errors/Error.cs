
namespace AuthService.Domain.Errors
{
    public record Error(ErrorCode code, string Message, object? metadata);
}
