

using GameEngineService.Domain.Entities;

namespace GameEngineService.Domain.Chess
{
    public interface IGameSession
    {
        public int Id { get; set; }
        void SetPlayers(User black, User white);
    }
}
