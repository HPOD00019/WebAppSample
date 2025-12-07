
using System.ComponentModel.DataAnnotations;
using AuthService.Domain.Services;
using MediatR;

namespace AuthService.Application.Commands.GenerateToken
{
    public record GenerateOrGetRefreshTokenCommand : IRequest<IResult<string>>
    {
        [Required] public int UserId { get; init; }
    }
}
