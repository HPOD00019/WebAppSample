using Chess;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Services;
using GameEngineService.Infrastructure.DTOs;
using GameEngineService.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GameEngineService.Infrastructure.SignalRws
{
    public class SignalRSocketService : ISocketService<ChessGameMessage>
    {
        private readonly IHubContext<PlayerHub> _players;

        public event EventHandler<ChessGameMessage> OnclientMessage;

        public SignalRSocketService(IHubContext<PlayerHub> players)
        {
            _players = players;
        }

        public void HandleClientMessage(ChessGameMessage message)
        {
            OnclientMessage.Invoke(this, message);
        }

        public void SendMessage(ChessGameMessage message)
        {
            var g = _players.Clients.Group("TEST_ROOM");
            var user = new UserDTO(message.Issuer.Id.ToString());
            var move = new ChessMoveDTO(message.Move.GetSanNotation());
            var msg = new ChessGameMessageDTO(message.GameId.ToString(), message.MessageType.ToString(), user, move);
            g.SendAsync("OnServerMessage", msg);
        }

        public void SendPosition(string posision)
        {
            var g = _players.Clients.Group("TEST_ROOM");
            g.SendAsync("ReceiveNewPosition", posision);
        }
    }
}
