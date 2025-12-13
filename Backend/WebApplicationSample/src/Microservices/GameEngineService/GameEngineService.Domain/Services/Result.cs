using GameEngineService.Domain.Services;
using GameEngineService.Domain.Errors;

namespace GameEngineService.Application.Services
{
    public class Result<T> : IResult<T>
    {
        public bool IsSuccess { get; }
        public Error? EmergedError { get; }
        public string? ErrorMessage { get; }

        public T? Value { get; }

        private Result (T value)
        {
            this.Value = value;
            this.IsSuccess = true;
        }
        
        private Result(Error error, string? Message)
        {
            this.EmergedError = error;
            this.ErrorMessage = Message;
        }
        public static Result<T> OnSuccess(T value) => new Result<T>(value);
        public static Result<T> OnFailure(Error error, string? message = null)  => new Result<T>(error, message);

       
    }
}
