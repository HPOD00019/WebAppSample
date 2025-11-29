
using GameEngineService.Domain.Chess;

namespace GameEngineService.Infrastructure.DTOs
{
    public class ChessMoveDTO
    {
        public string San { get; set; }
        public ChessMoveDTO() { }
        public ChessMoveDTO(ChessMove? move)
        {
            San = move?.GetSanNotation();
        }
        public ChessMoveDTO(string san)
        {
            San = san;
        }
        public static ChessMove? ToChessMove(ChessMoveDTO dto)
        {
            if(dto == null) return null;

            var move = new ChessMove(dto.San);
            return move;
        }
    }
}
