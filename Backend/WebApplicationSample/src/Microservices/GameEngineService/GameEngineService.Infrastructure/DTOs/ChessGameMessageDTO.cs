

using GameEngineService.Domain.Connections;

namespace GameEngineService.Infrastructure.DTOs
{
    public class ChessGameMessageDTO
    {
        public string GameId { get; set; }
        public string Fen { get; set; }
        public string MessageType { get; set; }
        public UserDTO Issuer { get; set; }
        public ChessMoveDTO? Move { get; set; }
        public ChessGameMessageDTO() { }
        public ChessGameMessageDTO(string gameId, string messageType, UserDTO issuer, ChessMoveDTO? move, string fen)
        {
            GameId = gameId;
            Fen = fen;
            MessageType = messageType;
            Issuer = issuer;
            Move = move;
        }
        public ChessGameMessageDTO (ChessGameMessage msg)
        {
            Fen = msg.Fen;
            GameId = msg.GameId.ToString();
            MessageType = msg.MessageType.ToString();
            Issuer = new UserDTO(msg.Issuer);
            Move = new ChessMoveDTO(msg.Move);
        }
        public static ChessGameMessage ToChessGameMessage(ChessGameMessageDTO message)
        {
            var id = Int32.Parse(message.GameId);
            ChessMessageType messageType = (ChessMessageType)Enum.Parse(typeof(ChessMessageType), message.MessageType, true);
            var issuer = UserDTO.ToUser(message.Issuer);
            var fen = message.Fen;
            var ans = new ChessGameMessage(id, messageType, issuer, ChessMoveDTO.ToChessMove(message.Move), fen);
            return ans;
        }
    }
}
