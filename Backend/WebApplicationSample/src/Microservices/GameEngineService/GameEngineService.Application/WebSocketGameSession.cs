using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Entities;

namespace GameEngineService.Application
{
    public class WebSocketGameSession : IGameSession, IDisposable
    {
        private IGameConnection _connection;
        private IChessCore _chessCore;

        private User _blackPlayer;
        private User _whitePlayer;

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WebSocketGameSession(IGameConnection connection, IChessCore chessCore)
        {
            _connection = connection;
            _chessCore = chessCore;

            _connection.OnPlayerMove += PlayerMoveHandler;
        }

        private void PlayerMoveHandler(object? sender, ChessGameMessage e)
        {
           var isMoveValid = _chessCore.ValidateMove(e.Move);
           if (!isMoveValid)
           {
                throw new NotImplementedException("Move was invalid!");
           }
           _chessCore.Move(e.Move);

            _connection.SendMessage(e);
        }
        
        public void Dispose()
        {
            if (_connection != null) _connection.OnPlayerMove -= PlayerMoveHandler;
        }

        public void SetPlayers(User black, User white)
        {
            _blackPlayer = black;
            _whitePlayer = white;
        }
    }
}