using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineService.Domain.TimeControls;

namespace GameEngineService.Domain.Entities
{
    public record MatchResultDTO
    {
        [Required] public int matchId { get; init; }
        [Required] public MatchResult result {  get; init; }
        [Required] public TimeControl control { get; init; }
    }
}
