using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Entities;

namespace GameEngineService.Domain.Connections
{
    public class ChessGameMessage
    {
        public int GameId { get; init; }
        public string Fen { get; set; }
        public ChessMessageType MessageType { get; init; }
        public User Issuer { get; init; }
        public ChessMove? Move { get; set; }
        public ChessGameMessage(int gameId, ChessMessageType type, User issuer, ChessMove? move, string fen)
        {
            Fen = fen;
            GameId = gameId;
            MessageType = type;
            Issuer = issuer;
            Move = move;
        }
        
    }
}
