
namespace GameEngineService.Domain.Connections
{
    public interface IGameConnection
    {
        public event EventHandler<ChessGameMessage> OnPlayerMove;
        public event EventHandler<ChessGameMessage> OnPlayerSuggestDraw;
        public event EventHandler<ChessGameMessage> OnPlayerResign;
        public void OnIntialize(int SessionId);
        public void SendMessage(ChessGameMessage message);  

    }
}
