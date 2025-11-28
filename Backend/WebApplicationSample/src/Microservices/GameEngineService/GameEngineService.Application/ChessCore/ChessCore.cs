using Chess;
using GameEngineService.Domain.Chess;

namespace GameEngineService.Application.ChessCore
{
    public class ChessCore : IChessCore
    {
        Stack<ChessMove> _history;
        private ChessBoard _board;
        public ChessCore()
        {
            _board = new ChessBoard();
            _history = new Stack<ChessMove>();
        }
        private void OnMove(ChessMove move)
        {
            _history.Push(move);
        }
        private void RemoveLastMove()
        {
            _board.Cancel();
            _history.Pop();
        }
        public void Reset()
        {
            _board = new ChessBoard();
        }
        public string GetCurrentPositionInFen()
        {
            string position = _board.ToFen();
            return position;
        }

        public bool IsCheck(ChessMove move)
        {
             _board.Move(move.GetSanNotation());
            bool ans = _board.BlackKingChecked | _board.WhiteKingChecked;
            _board.Cancel();
            return ans;
        }

        public bool IsDraw(ChessMove move)
        {
            _board.Move(move.GetSanNotation());
            var endgame = _board.EndGame;
            if(endgame != null ) return true;
            return false;
        }

        public bool IsMate(ChessMove move)
        {
            var ans = false;
            var end = _board.EndGame;
            ans = end?.EndgameType == EndgameType.Checkmate;
            return ans;
        }

        public bool IsStaleMate(ChessMove move)
        {
            var ans = false;
            var end = _board.EndGame;
            ans = end?.EndgameType == EndgameType.Stalemate;
            return ans;
        }

        public bool ValidateMove(ChessMove move)
        {
            return _board.IsValidMove(move.GetSanNotation());

        }

        public void Move(ChessMove move)
        {
            if (!_board.IsValidMove(move.GetSanNotation())) throw new Exception("Invalid move was passed!");
            _board.Move(move.GetSanNotation());
        }
    }
}
