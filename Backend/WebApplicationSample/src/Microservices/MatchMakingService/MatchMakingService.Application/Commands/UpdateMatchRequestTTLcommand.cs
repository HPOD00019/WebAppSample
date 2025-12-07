
using System.ComponentModel.DataAnnotations;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.TimeControls;
using MediatR;

namespace MatchMakingService.Application.Commands
{
    public record UpdateMatchRequestTTLcommand : IRequest<IResult<TimeSpan>>
    {
        [Required] public TimeControl control;
        [Required] public int IssuerId { get; set; }
    }
}
