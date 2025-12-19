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
    public record HandleMatchCreatedCommand : IRequest<IResult<bool>>
    {
        [Required] public string Link { get; init; }
        [Required] public int Id { get; init; }
    }
}
