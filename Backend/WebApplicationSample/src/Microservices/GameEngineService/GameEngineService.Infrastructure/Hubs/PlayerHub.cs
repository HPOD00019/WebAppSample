
using GameEngineService.Domain;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Services;
using GameEngineService.Infrastructure.DTOs;
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
        
        public async Task OnClientGameMessage (ChessGameMessageDTO message)
        {
            var chessMessage = ChessGameMessageDTO.ToChessGameMessage(message);
            _socketService.HandleClientMessage(chessMessage);
        }

        public async Task SendGameMessage(ChessGameMessage message)
        {

        }
    }
}
