
namespace GameEngineService.Domain.Errors
{
    public record Error(ErrorCode code, string? Message = null, object? metadata = null);
}
