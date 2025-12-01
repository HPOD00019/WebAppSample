
namespace MatchMakingService.Domain.Entities
{
    public interface IResult<T>
    {
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }
        public Error? EmergedError { get; }
        public T? Value { get; }
    }
}
