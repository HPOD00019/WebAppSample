
using GameEngineService.Domain.Chess;

namespace GameEngineService.Application.ChessCore
{
    public class ChessGame : IChessGame
    {
        private List<ChessMove> _moves;

        public void OnMove(ChessMove move)
        {
            throw new NotImplementedException();
        }
    }
}
