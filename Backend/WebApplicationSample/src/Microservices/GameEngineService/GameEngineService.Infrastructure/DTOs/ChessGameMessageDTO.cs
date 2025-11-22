
using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Entities;

namespace GameEngineService.Infrastructure.DTOs
{
    public class ChessGameMessageDTO
    {
        public string GameId { get; set; }
        public string MessageType { get; set; }
        public UserDTO Issuer { get; set; }
        public ChessMoveDTO? Move { get; set; }
        public ChessGameMessageDTO() { }
        public ChessGameMessageDTO(string gameId, string messageType, UserDTO issuer, ChessMoveDTO? move)
        {
            GameId = gameId;
            MessageType = messageType;
            Issuer = issuer;
            Move = move;
        }
        public static ChessGameMessage ToChessGameMessage(ChessGameMessageDTO message)
        {
            var id = Int32.Parse(message.GameId);
            ChessMessageType messageType = (ChessMessageType)Enum.Parse(typeof(ChessMessageType), message.MessageType, true);
            var issuer = UserDTO.ToUser(message.Issuer);

            var ans = new ChessGameMessage(id, messageType, issuer, ChessMoveDTO.ToChessMove(message.Move));
            return ans;
        }
    }
}
