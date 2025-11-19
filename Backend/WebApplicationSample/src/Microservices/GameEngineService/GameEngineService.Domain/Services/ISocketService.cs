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
        public event EventHandler<Tmessage> OnclientMessage;
        public void SendMessage(Tmessage message);
        public void HandleClientMessage(Tmessage message);
    }
}
