using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineService.Domain.Connections
{
    public interface IGameConnection
    {
        public event EventHandler<ChessGameMessage> OnReceiveMessage;
        void SendMessage(ChessGameMessage message);

    }
}
