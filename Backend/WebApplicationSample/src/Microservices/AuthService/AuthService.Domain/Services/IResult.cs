﻿using AuthService.Domain.Errors;

namespace AuthService.Domain.Services
{
    public interface IResult<T>
    {
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }
        public T? Value { get; }
    }
}
