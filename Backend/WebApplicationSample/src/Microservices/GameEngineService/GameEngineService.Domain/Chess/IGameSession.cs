

using GameEngineService.Domain.Entities;

namespace GameEngineService.Domain.Chess
{
    public interface IGameSession
    {
        bool AddUser(User user);
        void OnReset();
        void OnUserLeft(User user);
        void SendPosition();
    }
}
