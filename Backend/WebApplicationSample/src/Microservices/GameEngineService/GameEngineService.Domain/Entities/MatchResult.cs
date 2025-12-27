using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineService.Domain.Entities
{
    public enum MatchResult
    {
        WhiteWinOnTime,
        WhiteWinByResignation,
        WhiteWinByCheckMate,
        StaleMate,
        DrawByAgreemant,
        BlackWinOnTime,
        BlackWinByResignation,
        BlackWinByCheckMate,
    }
}
