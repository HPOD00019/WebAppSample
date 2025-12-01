
using System.ComponentModel.DataAnnotations;
using MatchMakingService.Domain.Entities;
using MediatR;

namespace MatchMakingService.Application.Commands
{
    public record UpdateMatchRequestTTLcommand : IRequest<IResult<TimeSpan>>
    {
        [Required] public int IssuerId { get; set; }
    }
}
