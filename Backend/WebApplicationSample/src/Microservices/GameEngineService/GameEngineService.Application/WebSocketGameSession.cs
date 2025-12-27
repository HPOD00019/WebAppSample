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

        public event EventHandler<MatchResultDTO> OnMatchEnd;

        public User BlackPlayer { get; set; }
        public User WhitePlayer { get; set; }

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
            if ((e.Issuer.Id == WhitePlayer.Id && !_isWhiteMove) || (e.Issuer.Id == BlackPlayer.Id && _isWhiteMove)) return;
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
            if (e.Issuer.Id == BlackPlayer.Id)
            {
                var remaningTime = _timeManager.OnBlackMove(); 
                if (remaningTime <= TimeSpan.Zero)
                {
                    var results = new MatchResultDTO
                    {
                        matchId = Id,
                        control = _control,
                        result = MatchResult.WhiteWinOnTime,
                    };
                    OnMatchEnd.Invoke(this, results);
                    _connection.SendMessage(results.result,  Id);
                    return;
                }
                var whiteTime = _timeManager.WhiteTime;
                _message.BlackRemainingTime = (int)remaningTime.TotalMilliseconds;
                _message.WhiteRemainingTime = (int)whiteTime.TotalMilliseconds;
                
            }
            else if(e.Issuer.Id == WhitePlayer.Id)
            {
                var remaningTime = _timeManager.OnWhiteMove();
                if (remaningTime <= TimeSpan.Zero)
                {
                    var results = new MatchResultDTO
                    {
                        matchId = Id,
                        control = _control,
                        result = MatchResult.BlackWinOnTime,
                    };
                    OnMatchEnd.Invoke(this, results);
                    _connection.SendMessage(results.result, Id);
                    return;

                }
                var blackTime = _timeManager.BlackTime;
                _message.WhiteRemainingTime = (int)remaningTime.TotalMilliseconds;
                _message.BlackRemainingTime = (int)remaningTime.TotalMilliseconds;
            }
            var res = _chessCore.GetMatchResult();
            if (res != null)
            {
                var result = new MatchResultDTO
                {
                    matchId = Id,
                    control = _control,
                    result = res.Value,
                };
                OnMatchEnd.Invoke(this, result);
                _connection.SendMessage(res.Value, Id);
                return;
            }
            
            _connection.SendMessage(_message);
        }
        
        public void Dispose()
        {
            if (_connection != null) _connection.OnPlayerMove -= PlayerMoveHandler;
        }

        public void SetPlayers(User black, User white)
        {
            BlackPlayer = black;
            WhitePlayer = white;
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