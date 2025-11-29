using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineService.Domain.Connections;

namespace GameEngineService.Domain.Services
{
    public interface ISocketService<Tmessage>
    {
        public void UnSubscribeOnClientMessage(int id);
        public void SubscribeOnClientMessage(int id, Action<ChessGameMessage> handler);
        public void SendMessage(Tmessage message);
        public void HandleClientMessage(Tmessage message);
    }
}
