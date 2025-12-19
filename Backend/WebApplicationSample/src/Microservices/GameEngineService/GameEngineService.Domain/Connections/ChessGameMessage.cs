using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Entities;

namespace GameEngineService.Domain.Connections
{
    public class ChessGameMessage
    {
        public int GameId { get; set; }
        public string Fen { get; set; }
        public ChessMessageType MessageType { get; init; }
        public User Issuer { get; init; }
        public ChessMove? Move { get; set; }
        public int? WhiteRemainingTime { get; set; }
        public int? BlackRemainingTime { get; set; }
        public ChessGameMessage(int gameId, ChessMessageType type, User issuer, ChessMove? move, string fen, int? whiteRemainingTime = null, int? blackRemainingTime = null)
        {
            Fen = fen;
            GameId = gameId;
            MessageType = type;
            Issuer = issuer;
            Move = move;
            WhiteRemainingTime = whiteRemainingTime;
            BlackRemainingTime = blackRemainingTime;
        }
        public ChessGameMessage(ChessGameMessage message)
        {
            GameId = message.GameId;
            MessageType = message.MessageType;
            Issuer = message.Issuer;
            Move = message.Move;
            Fen = message.Fen;
            WhiteRemainingTime = message.WhiteRemainingTime;
            BlackRemainingTime = message.BlackRemainingTime;
        }
    }
}
