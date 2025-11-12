
using System.ComponentModel.DataAnnotations;
using AuthService.Domain.Services;
using MediatR;

namespace AuthService.Application.Commands.GenerateToken
{
    public record GenerateRefreshTokenCommand : IRequest<IResult<string>>
    {
        [Required] public Guid UserId { get; init; }
    }
}
