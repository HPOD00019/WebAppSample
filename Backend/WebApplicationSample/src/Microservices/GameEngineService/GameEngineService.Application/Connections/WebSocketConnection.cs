using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineService.Domain.Connections;

namespace GameEngineService.Application.Connections
{
    public class WebSocketConnection : IGameConnection
    {
        public event EventHandler<ChessGameMessage> OnReceiveMessage;

        public WebSocketConnection()
        {

        }

        public void SendMessage(ChessGameMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
