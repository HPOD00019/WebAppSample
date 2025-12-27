using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BotService.Domain.Entities
{
    public interface IChessEngine
    {
        string GetBestMove(string fen, int? elo = null);
        string GetEvaluation(string fen);
    }
}
