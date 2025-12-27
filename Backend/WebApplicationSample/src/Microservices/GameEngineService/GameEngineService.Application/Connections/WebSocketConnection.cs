
using System.Runtime.CompilerServices;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Entities;
using GameEngineService.Domain.Services;

namespace GameEngineService.Application.Connections
{
    public class WebSocketConnection : IGameConnection, IDisposable
    {
        private ISocketService<ChessGameMessage> _socketService;
        public event EventHandler<ChessGameMessage> OnPlayerMove;
        public event EventHandler<ChessGameMessage> OnPlayerSuggestDraw;
        public event EventHandler<ChessGameMessage> OnPlayerResign;

        public WebSocketConnection(ISocketService<ChessGameMessage> socket)
        {
            _socketService = socket;
        }
        
        public void SendMessage(ChessGameMessage message)
        {
            _socketService.SendMessage(message);
        }

        private void MessageReceivedHandler(ChessGameMessage message)
        {
            switch (message.MessageType)
            {
                case ChessMessageType.Move:
                    OnPlayerMove.Invoke(this, message); 
                    break;

                case ChessMessageType.SuggestDraw:
                    OnPlayerSuggestDraw.Invoke(this, message);
                    break;

                case ChessMessageType.Resign:
                    OnPlayerResign.Invoke(this, message);
                    break;
            }
        }

        public void Dispose()
        {

        }

        public void OnIntialize(int SessionId)
        {
            _socketService.SubscribeOnClientMessage(SessionId, MessageReceivedHandler);
        }

        public void SendMessage(MatchResult result, int matchId)
        {
            _socketService.OnGameEndMessage(result, matchId);
        }
    }
}
