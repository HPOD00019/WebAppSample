
using System.Timers;

namespace GameEngineService.Application.ChessCore
{
    public class CountDownTimer
    {

        private double _tickTime = 100;
        private System.Timers.Timer _timer;
        public int RemainingTime { get; set; }
        public Action OnTimeElapsed { get; set; }

        public CountDownTimer() { }
        public void Start(int? TotalTime = null)
        {
            if(TotalTime != null)
            {
                RemainingTime = TotalTime.Value;
            }
            if(_timer != null)
            {
                Stop();
            }
            _timer = new System.Timers.Timer(_tickTime);
            _timer.Elapsed += ElapsedHandler;
            _timer.Start();
        }
        public void Stop()
        {
            _timer.Elapsed -= ElapsedHandler;
            _timer.Dispose();
            _timer = null;
        }
        private void ElapsedHandler(object? sender, ElapsedEventArgs e)
        {
            RemainingTime -= (int)_tickTime;
            if(RemainingTime <= 0)
            {
                Stop();
                OnTimeElapsed.Invoke();
            }
        }
    }
}
