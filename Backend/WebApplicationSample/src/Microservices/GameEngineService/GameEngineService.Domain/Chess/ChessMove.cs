

namespace GameEngineService.Domain.Chess
{
    public class ChessMove
    {
        private readonly string _san;
        public ChessMove(string sanNotation)
        {
            _san = sanNotation;
        }

        public string GetSanNotation()
        {
            return _san;
        }
    }
}
