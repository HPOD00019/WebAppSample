using System.Collections.Concurrent;
using GameEngineService.Domain;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Entities;
using GameEngineService.Domain.Services;
using GameEngineService.Infrastructure.DTOs;
using GameEngineService.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GameEngineService.Infrastructure.SignalRws
{
    public class SignalRSocketService : ISocketService<ChessGameMessage>
    {
        private readonly IHubContext<PlayerHub> _players;
        private ConcurrentDictionary<int, Action<ChessGameMessage>> _messageReceivedHandlers = new ConcurrentDictionary<int, Action<ChessGameMessage>>();
        private SessionManager _manager;
        public SignalRSocketService(SessionManager manager, IHubContext<PlayerHub> hub)
        {
            _players = hub;
            _manager = manager;
        }

        public void HandleClientMessage(ChessGameMessage message)
        {
            var _message = new ChessGameMessage(message);
            var userId = message.Issuer.Id;
            var gameId = _manager.GetSessionByPlayerId(userId);
            _message.GameId = gameId;
            var isHandlerNotNull = _messageReceivedHandlers.TryGetValue(gameId, out var handler);
            if (isHandlerNotNull)
            {
                handler.Invoke(_message);
            }
        }

        public void SendMessage(ChessGameMessage message)
        {
            var _message = new ChessGameMessageDTO(message);
            var group = _players.Clients.Group(message.GameId.ToString());
            group.SendAsync("OnServerMessage", _message);
        }
        public void OnGameEndMessage(MatchResult result, int matchId)
        {
            var group = _players.Clients.Group(matchId.ToString());
            group.SendAsync("OnGameEnd", result.ToString());
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
