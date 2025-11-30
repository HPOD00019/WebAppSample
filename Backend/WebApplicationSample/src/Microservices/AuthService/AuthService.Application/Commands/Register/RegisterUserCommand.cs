using AuthService.Application.Services;
using AuthService.Domain.Services;
using MediatR;
using System.ComponentModel.DataAnnotations; 


namespace AuthService.Application.Commands.Register
{
    public record RegisterUserCommand : IRequest<IResult<int>>
    {
        [Required] public string UserName { get; init; }
        [Required] public string Password { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }
    }
}
