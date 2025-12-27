using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.TimeControls;
using MediatR;

namespace MatchMakingService.Application.Commands
{
    public record MatchFinishedCommand : IRequest<IResult<bool>>
    {
        [Required] public TimeControl control { get; init; }
        [Required] public int matchId { get; init; }
        [Required] public MatchResult result { get; init; }

    }
}
