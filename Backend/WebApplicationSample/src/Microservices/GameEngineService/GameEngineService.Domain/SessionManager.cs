using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineService.Domain.Chess;

namespace GameEngineService.Domain
{
    public class SessionManager
    {
        private ConcurrentDictionary<int, IGameSession> _sessions;
        public IGameSession GetSession(int sessionId)
        {
            _sessions.TryGetValue(sessionId, out var session);
            return session;
        }
        public void AddSession(IGameSession session)
        {
            var id = session.Id;
            _sessions.TryAdd(id, session);
        }
        public void RemoveSession(IGameSession session)
        {
            var id = session.Id;
            _sessions.TryRemove(id, out _);
        }

    }
}
