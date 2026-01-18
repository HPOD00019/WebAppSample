using System;
using System.Diagnostics;
using GameEngineService.Application.ChessCore;
using GameEngineService.Domain.Chess.GameEngineService.Domain.Chess;

namespace GameEngineService.Domain.Chess
{
    public class BasicTimeManager : ITimeManager
    {
        private CountDownTimer _whiteTimer = new CountDownTimer();
        private CountDownTimer _blackTimer = new CountDownTimer();
        public BasicTimeManager( TimeSpan Time, TimeSpan increment, Action OnTimeElapsed)
        {
            Increment = increment;
            _whiteTimer.RemainingTime = (int)Time.TotalMilliseconds;
            _blackTimer.RemainingTime = (int)Time.TotalMilliseconds;
            _whiteTimer.OnTimeElapsed += OnTimeElapsed;
            _blackTimer.OnTimeElapsed += OnTimeElapsed;
        }
        public TimeSpan WhiteTime 
        {   get
            {
                var remainTime = _whiteTimer.RemainingTime;
                var ans = TimeSpan.FromMilliseconds(remainTime);
                return ans;
            } 
        }

        public TimeSpan BlackTime
        {
            get
            {
                var remainTime = _blackTimer.RemainingTime;
                var ans = TimeSpan.FromMilliseconds(remainTime);
                return ans;
            }
        }

        public TimeSpan Increment { get; set; }

        public TimeSpan OnBlackMove()
        {
            _blackTimer.Stop();
            _blackTimer.RemainingTime += (int)Increment.TotalMilliseconds;
            _whiteTimer.Start();
            return TimeSpan.FromMilliseconds(_blackTimer.RemainingTime);
        }

        public void OnGameStart()
        {
            _whiteTimer.Start();
            
        }

        public TimeSpan OnWhiteMove()
        {
            _whiteTimer.Stop();
            _whiteTimer.RemainingTime += (int)Increment.TotalMilliseconds;
            _blackTimer.Start();
            return TimeSpan.FromMilliseconds(_whiteTimer.RemainingTime);
        }
    }
}