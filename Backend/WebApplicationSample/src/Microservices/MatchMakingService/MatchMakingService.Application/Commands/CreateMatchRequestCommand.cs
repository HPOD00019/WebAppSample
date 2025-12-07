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
    public record CreateMatchRequestCommand : IRequest<IResult<TimeSpan>>
    {
        public Tuple<int, int>? FromToSettings { get; set; }
        [Required] public TimeControl control;
        [Required] public User Issuer { get; set; }
    }
}
