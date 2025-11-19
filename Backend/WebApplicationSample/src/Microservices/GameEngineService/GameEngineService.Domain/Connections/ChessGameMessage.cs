using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Entities;

namespace GameEngineService.Domain.Connections
{
    public class ChessGameMessage
    {
        public required Guid GameId { get; init; }
        public required ChessMessageType MessageType { get; init; }
        public required User Issuer { get; init; }
        public ChessMove? Move { get; set; }
        
    }
}
