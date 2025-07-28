using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
