using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Chess.GameEngineService.Domain.Chess;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Entities;
using GameEngineService.Domain.TimeControls;

namespace GameEngineService.Application
{
    public class WebSocketGameSession : IGameSession, IDisposable
    {
        private ITimeManager _timeManager;
        private IGameConnection _connection;
        private IChessCore _chessCore;
        private bool _isWhiteMove = true;
        private User _blackPlayer { get; set; }
        private User _whitePlayer { get; set; }

        private TimeControl _control { get; set; }
        public int Id { get; set; }

        public WebSocketGameSession(IGameConnection connection, IChessCore chessCore, int? id = null)
        {
            _connection = connection;
            _chessCore = chessCore;
            Id = id.Value;
            _connection.OnPlayerMove += PlayerMoveHandler;
            _connection.OnIntialize(Id);

        }

        private void PlayerMoveHandler(object? sender, ChessGameMessage e)
        {
            if ((e.Issuer.Id == _whitePlayer.Id && !_isWhiteMove) || (e.Issuer.Id == _blackPlayer.Id && _isWhiteMove)) return;
            var isMoveValid = _chessCore.ValidateMove(e.Move);
            if (!isMoveValid)
            {
                throw new NotImplementedException("Move was invalid!");
            }
            if (_chessCore.isStartPosition())
            {
                _timeManager.OnGameStart();
            }
            _chessCore.Move(e.Move);
            _isWhiteMove = !_isWhiteMove;
            var _message = new ChessGameMessage(e);
            if (e.Issuer.Id == _blackPlayer.Id)
            {
                var remaningTime = _timeManager.OnBlackMove();
                if (remaningTime <= TimeSpan.Zero) Console.WriteLine("WHITE WINS");
                var whiteTime = _timeManager.WhiteTime;
                _message.BlackRemainingTime = (int)remaningTime.TotalMilliseconds;
                _message.WhiteRemainingTime = (int)whiteTime.TotalMilliseconds;
            }
            else
            {
                var remaningTime = _timeManager.OnWhiteMove();
                if (remaningTime <= TimeSpan.Zero) Console.WriteLine("BLACK WINS");
                var blackTime = _timeManager.BlackTime;
                _message.WhiteRemainingTime = (int)remaningTime.TotalMilliseconds;
                _message.BlackRemainingTime = (int)remaningTime.TotalMilliseconds;
            }

            _connection.SendMessage(_message);
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

        public void SetTimeControl(TimeControl control)
        {
            _control = control;
            TimeControlTransitions.TryParseTimeControl(control,out var time, out var increment);
            _timeManager = new BasicTimeManager(time, time, increment);
        }

        public string GetCurrentPositionFen()
        {
            var position = _chessCore.GetCurrentPositionInFen();
            return position;
            
        }
    }
}