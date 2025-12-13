namespace MatchMakingService.Api.MassTransit.Messages.Commands
{
    public record CreateMatchSagaCommand
    {
        public Guid CorrelationId { get; init; }
        public int MatchId { get; init; }
        public int WhiteOpponent {  get; init; }
        public int BlackOpponent { get; init; }

        public CreateMatchSagaCommand() { }
    }
}
