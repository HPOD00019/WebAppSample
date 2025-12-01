using AuthService.Application.Services;
using AuthService.Domain.Models;
using AuthService.Domain.Services;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace AuthService.Application.Commands.Register
{
    public record LoginUserCommand : IRequest<IResult<User>>
    {
        [Required] public string UserName { get; init; }
        [Required] public string Password { get; init; }
    }
}
