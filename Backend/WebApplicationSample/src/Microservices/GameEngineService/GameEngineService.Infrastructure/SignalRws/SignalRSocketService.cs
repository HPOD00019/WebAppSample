using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Services;
using GameEngineService.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GameEngineService.Infrastructure.SignalRws
{
    public class SignalRSocketService : ISocketService<ChessGameMessage>
    {
        private readonly IHubContext<PlayerHub> _players;

        public event EventHandler<ChessGameMessage> OnclientMessage;

        public SignalRSocketService()
        {

        }

        public void HandleClientMessage(ChessGameMessage message)
        {
            OnclientMessage.Invoke(this, message);
        }

        public void SendMessage(ChessGameMessage message)
        {
            _players.Clients.Group(message.GameId.ToString()).SendAsync("OnServerMessage", message);
        }
    }
}
