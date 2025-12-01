
namespace MatchMakingService.Domain.Entities
{
    public record Error(ErrorCode code, string? Message = null, object? metadata = null);
}
