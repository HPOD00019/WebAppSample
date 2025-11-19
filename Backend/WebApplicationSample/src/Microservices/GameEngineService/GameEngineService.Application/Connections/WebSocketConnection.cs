
using GameEngineService.Domain.Connections;
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
            _socketService.OnclientMessage += this.MessageReceivedHandler;
        }

        public void SendMessage(ChessGameMessage message)
        {
            throw new NotImplementedException();
        }

        private void MessageReceivedHandler(object? sender,  ChessGameMessage message)
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
            _socketService.OnclientMessage -= this.MessageReceivedHandler;
        }
    }
}
