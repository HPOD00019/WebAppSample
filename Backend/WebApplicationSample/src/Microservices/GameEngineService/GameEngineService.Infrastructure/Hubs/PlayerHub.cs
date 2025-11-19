
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Services;
using Microsoft.AspNetCore.SignalR;

namespace GameEngineService.Infrastructure.Hubs
{
    public class PlayerHub : Hub
    {
        private readonly ISocketService<ChessGameMessage> _socketService;
        public PlayerHub(ISocketService<ChessGameMessage> socket)
        {
            _socketService = socket;
        }

        public async Task OnClientGameMessage (ChessGameMessage message)
        {
            _socketService.HandleClientMessage (message);
        }

        public async Task SendGameMessage(ChessGameMessage message)
        {

        }
    }
}
