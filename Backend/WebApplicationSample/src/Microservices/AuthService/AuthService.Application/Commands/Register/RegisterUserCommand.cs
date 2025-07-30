using MediatR;
using System.ComponentModel.DataAnnotations; 


namespace AuthService.Application.Commands.Register
{
    public record RegisterUserCommand : IRequest<Guid>
    {
        [Required] public string UserName { get; init; }
        [Required] public string Password { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }
    }
}
