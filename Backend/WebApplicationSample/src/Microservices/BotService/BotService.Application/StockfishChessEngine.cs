using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotService.Domain.Entities;
using Sharpfish;

namespace BotService.Application
{
    public class StockfishChessEngine : IChessEngine
    {
        private IStockfishEngine _engine;

        public string GetBestMove(string fen, int? elo = null)
        {
            throw new NotImplementedException();
        }

        public string GetEvaluation(string fen)
        {
            throw new NotImplementedException();
        }
    }
}
