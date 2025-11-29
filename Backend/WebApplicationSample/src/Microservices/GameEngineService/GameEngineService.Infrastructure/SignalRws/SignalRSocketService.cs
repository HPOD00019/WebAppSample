using System.Collections.Concurrent;
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
        private ConcurrentDictionary<int, Action<ChessGameMessage>> _messageReceivedHandlers;
        public SignalRSocketService()
        {

        }

        public void HandleClientMessage(ChessGameMessage message)
        {
            var id = message.GameId;
            var isHandlerNotNull = _messageReceivedHandlers.TryGetValue(id, out var handler);
            if (isHandlerNotNull)
            {
                handler.Invoke(message);
            }
        }

        public void SendMessage(ChessGameMessage message)
        {
            var _message = new ChessGameMessageDTO(message);
            _players.Clients.Group(message.GameId.ToString()).SendAsync("OnServerMessage", _message);
        }

        public void SubscribeOnClientMessage(int id, Action<ChessGameMessage> handler)
        {
            _messageReceivedHandlers.TryAdd(id, handler);
        }

        public void UnSubscribeOnClientMessage(int id)
        {
            _messageReceivedHandlers.TryRemove(id, out _);
        }

    }
}
