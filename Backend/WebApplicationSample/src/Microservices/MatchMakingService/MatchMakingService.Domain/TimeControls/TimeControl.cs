using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakingService.Domain.TimeControls

{
    public enum TimeControl
    {
        [GameType("Blitz")]
        Minutes_3_increment_2_seconds,

        [GameType("Blitz")]
        Minutes_5_increment_5_seconds,

        [GameType("Blitz")]
        Minutes_5_increment_none,

        [GameType("Rapid")]
        Minutes_10_inrement_none,

        [GameType("Rapid")]
        Minutes_15_increment_10_seconds,

        [GameType("Classical")]
        Minutes_45_increment_none,

        [GameType("Classical")]
        Minutes_60_increment_30_seconds,
    }
}
