using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Domain.Services;
using MediatR;

namespace AuthService.Application.Commands.GenerateToken
{
    public record GenerateAccessTokenCommand : IRequest<IResult<string>>
    {
        [Required] public string RefreshToken { get; init; }

    }
}
