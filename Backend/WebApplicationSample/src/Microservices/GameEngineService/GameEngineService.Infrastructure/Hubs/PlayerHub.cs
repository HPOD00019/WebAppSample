
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
        private readonly SessionManager _sessionManager;
        public PlayerHub(ISocketService<ChessGameMessage> socket, SessionManager manager)
        {
            _sessionManager = manager;
            _socketService = socket;
        }
        public async Task Register(int userId)
        {
            var sessionId = _sessionManager.GetSessionByPlayerId(userId);
            var session = _sessionManager.GetSession(sessionId);
            var position = session.GetCurrentPositionFen();
            await Clients.Caller.SendAsync("GetCurrentPosition", position);
            await Clients.Caller.SendAsync("OnServerMessage");
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId.ToString());
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
