using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Entities;

namespace GameEngineService.Application
{
    public class WebSocketGameSession : IGameSession, IDisposable
    {
        private IGameConnection _connection;
        private IChessCore _chessCore;

        private User _whitePlayer = null;
        private User _blackPlayer = null;

        private bool _isGameStarted;
        private string _roomName;
        public WebSocketGameSession(IGameConnection connection, IChessCore chessCore)
        {
            _connection = connection;
            _chessCore = chessCore;

            _connection.OnPlayerMove += PlayerMoveHandler;
        }

        private void PlayerMoveHandler(object? sender, ChessGameMessage e)
        {
            if (!_isGameStarted) return;

            var isMoveValid = _chessCore.ValidateMove(e.Move);
            if (!isMoveValid)
            {
                return;
            }

            _chessCore.Move(e.Move);
            _connection.SendMessage(e);
            SendPosition();

        }
        public void OnReset()
        {
            _chessCore.Reset();
            _whitePlayer = null;
            _blackPlayer = null;
            _isGameStarted = false;
        }
        public bool AddUser(User user)
        {
            SendPosition();
            if (_whitePlayer == null)
            {
                _whitePlayer = user;
                return true;
            }
            if(_blackPlayer == null)
            {
                _blackPlayer = user;
                _isGameStarted = true;
                return false;
                
            }
            throw new Exception("Sorry, only one active game is allowed for tests. Refresh your page and open (or also refresh) another one in browser.");
        }
        public void OnUserLeft(User user)
        {
            if (_blackPlayer?.Id == user.Id)
            {
                _blackPlayer = null;
            }
            if (_whitePlayer?.Id == user.Id)
            {
                _whitePlayer = null;
            }
            if(_blackPlayer == null &&  _whitePlayer == null)
            {
                OnReset();
            }
        }
        public void Dispose()
        {
            if(_connection != null) _connection.OnPlayerMove -= PlayerMoveHandler;
        }

        public void SendPosition()
        {
            var p = _chessCore.GetCurrentPositionInFen();
            if (p != null)
            {
                _connection.SendPosition(p);
            }
        }
    }
}
