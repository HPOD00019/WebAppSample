
using GameEngineService.Domain.Entities;

namespace GameEngineService.Domain.Chess
{
    public interface IChessCore 
    {
        MatchResult? GetMatchResult();
        bool ValidateMove(ChessMove move);
        bool IsCheck(ChessMove move);
        string GetCurrentPositionInFen();
        void Move(ChessMove move);
        bool isStartPosition();
    }
}
