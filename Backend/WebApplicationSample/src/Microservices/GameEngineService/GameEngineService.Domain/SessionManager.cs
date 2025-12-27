using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Entities;
using GameEngineService.Domain.Services;

namespace GameEngineService.Domain
{
    public class SessionManager
    {
        private ConcurrentDictionary<int, IGameSession> _sessions = new ConcurrentDictionary<int, IGameSession>();
        private ConcurrentDictionary<int, int> _players = new ConcurrentDictionary<int, int>();
        public IMatchMessageService matchMessageService { get; set; }
        public IGameSession GetSession(int sessionId)
        {
            _sessions.TryGetValue(sessionId, out var session);
            return session;
        }
        public int GetSessionByPlayerId(int playerId)
        {
            _players.TryGetValue(playerId, out var session);
            return session;
        }
        public void AddPlayer(int playerId, int sessionId)
        {
            _players.TryAdd(playerId, sessionId);
        }
        public void MatchEndHandler( object? sender, MatchResultDTO result)
        {
            matchMessageService.PublishMatchFinishedMessage(result);
            var sessionId = result.matchId;
            var session = GetSession(sessionId);
            _sessions.Remove(sessionId,out _);
            _players.Remove(session.BlackPlayer.Id, out _);
            _players.Remove(session.WhitePlayer.Id, out _);
            session.Dispose();
        }
        public void AddSession(IGameSession session)
        {
            var id = session.Id;
            _sessions.TryAdd(id, session);
            session.OnMatchEnd += MatchEndHandler;
        }
        public void RemoveSession(IGameSession session)
        {
            var id = session.Id;
            _sessions.TryRemove(id, out var removedSession);
            removedSession.Dispose();
        }

    }
}
