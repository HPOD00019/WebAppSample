using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotService.Domain.Entities;

namespace BotService.Application
{
    public class EngineManager : IEngineManager
    {
        private ICollection<IChessEngine> _engines;
        public IEngineManager GetFreeEngine()
        {
            throw new NotImplementedException();
        }
    }
}
