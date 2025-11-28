
namespace GameEngineService.Domain.Chess
{
    public interface IChessCore 
    {
        void Reset();
        bool ValidateMove(ChessMove move);
        bool IsCheck(ChessMove move);
        bool IsMate(ChessMove move);
        bool IsStaleMate(ChessMove move);
        bool IsDraw(ChessMove move);
        string GetCurrentPositionInFen();
        void Move(ChessMove move);
    }
}
