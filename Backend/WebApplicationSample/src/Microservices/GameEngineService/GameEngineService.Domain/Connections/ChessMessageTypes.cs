using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineService.Domain.Connections
{
    public enum ChessMessageType
    {
        SuggestDraw,
        Resign,
        Move,
    }
}
