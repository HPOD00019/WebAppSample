using System;
using System.Diagnostics;
using GameEngineService.Domain.Chess.GameEngineService.Domain.Chess;

namespace GameEngineService.Domain.Chess
{
    public class BasicTimeManager : ITimeManager
    {
        private TimeSpan _whiteRemainingTime;
        private TimeSpan _blackRemainingTime;
        private readonly Stopwatch _whiteTimer = new Stopwatch();
        private readonly Stopwatch _blackTimer = new Stopwatch();
        private readonly TimeSpan? _increment;
        private bool _isGameStarted;
        private bool _isWhiteMove;

        public TimeSpan WhiteTime 
        {
            get 
            {
                if (_isWhiteMove) return _whiteRemainingTime - _whiteTimer.Elapsed;
                else return _whiteRemainingTime;
            }
        }
        public TimeSpan BlackTime
        {
            get
            {
                if (!_isWhiteMove) return _blackRemainingTime - _blackTimer.Elapsed;
                else return _blackRemainingTime;
            }
        }
        public TimeSpan Increment => _increment ?? TimeSpan.Zero;

        public BasicTimeManager(TimeSpan whiteTime, TimeSpan blackTime)
            : this(whiteTime, blackTime, TimeSpan.Zero)
        {
        }

        public BasicTimeManager(TimeSpan whiteTime, TimeSpan blackTime, TimeSpan increment)
        {
            _whiteRemainingTime = whiteTime;
            _blackRemainingTime = blackTime;
            _increment = increment;
            _isGameStarted = false;
            _isWhiteMove = true;
        }

        public void OnGameStart()
        {
            if (_isGameStarted)
                return;

            _isGameStarted = true;
            _whiteTimer.Start();
        }

        public TimeSpan OnWhiteMove()
        {
            if (!_isGameStarted)
                throw new InvalidOperationException("Game has not started yet");

            if (!_isWhiteMove)
                throw new InvalidOperationException("Not White's turn to move");

            _whiteTimer.Stop();
            var elapsed = _whiteTimer.Elapsed;
            _whiteTimer.Reset();

            if (elapsed >= _whiteRemainingTime)
            {
                _whiteRemainingTime = TimeSpan.Zero;
                return TimeSpan.Zero;
            }

            _whiteRemainingTime = _whiteRemainingTime - elapsed;

            if (_increment.HasValue)
            {
                _whiteRemainingTime = _whiteRemainingTime + _increment.Value;
            }

            _isWhiteMove = false;
            _blackTimer.Start();

            return _whiteRemainingTime;
        }

        public TimeSpan OnBlackMove()
        {
            if (!_isGameStarted)
                throw new InvalidOperationException("Game has not started yet");

            if (_isWhiteMove)
                throw new InvalidOperationException("Not Black's turn to move");

            _blackTimer.Stop();
            var elapsed = _blackTimer.Elapsed;
            _blackTimer.Reset();

            if (elapsed >= _blackRemainingTime)
            {
                _blackRemainingTime = TimeSpan.Zero;
                return TimeSpan.Zero;
            }

            _blackRemainingTime = _blackRemainingTime - elapsed;

            if (_increment.HasValue)
            {
                _blackRemainingTime = _blackRemainingTime + _increment.Value;
            }

            _isWhiteMove = true;
            _whiteTimer.Start();

            return _blackRemainingTime;
        }
    }
}