
using System.Runtime.CompilerServices;
using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Entities;
using GameEngineService.Domain.Services;
using GameEngineService.Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace GameEngineService.Infrastructure.Hubs
{
    public class PlayerHub : Hub
    {
        private  User _user;
        private readonly ISocketService<ChessGameMessage> _socketService;
        private readonly IGameSession _session;
        public PlayerHub(ISocketService<ChessGameMessage> socket, IGameSession session)
        {
            _socketService = socket;
            _session = session;
        }
        public override async Task OnConnectedAsync()
        {
            //FOR_TEST

            await Groups.AddToGroupAsync(Context.ConnectionId, "TEST_ROOM");
            await base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.Items.TryGetValue("User", out var userObj) && userObj is User user)
            {
                _session.OnUserLeft(user);
            }

            return base.OnDisconnectedAsync(exception);
        }
        public async Task Register(int id)
        {
            
            var user = new User(id);
            _user = user;
            Context.Items["User"] = user;
            var isWhite = _session.AddUser(user);
            await Clients.Caller.SendAsync("ReceivePlayerColor", isWhite);

        }
        public async Task OnClientGameMessage (ChessGameMessageDTO message)
        {
            var chessMessage = ChessGameMessageDTO.ToChessGameMessage(message);
            _socketService.HandleClientMessage (chessMessage);
        }
        public async Task SendGameMessage(ChessGameMessage message)
        {

        }
    }
}
