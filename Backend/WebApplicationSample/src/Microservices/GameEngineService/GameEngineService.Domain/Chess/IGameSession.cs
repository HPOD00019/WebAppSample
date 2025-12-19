

using GameEngineService.Domain.Entities;
using GameEngineService.Domain.TimeControls;

namespace GameEngineService.Domain.Chess
{
    public interface IGameSession : IDisposable
    {
        public string GetCurrentPositionFen();
        public int Id { get; set; }
        void SetPlayers(User black, User white);
        public void SetTimeControl(TimeControl control);
    }
}
