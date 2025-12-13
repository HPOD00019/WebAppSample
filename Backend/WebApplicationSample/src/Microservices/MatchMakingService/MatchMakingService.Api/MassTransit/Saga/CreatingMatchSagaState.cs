using MassTransit;

namespace MatchMakingService.Api.MassTransit.Saga
{
    public class CreatingMatchSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set ; }
        public string CurrentState { get; set; }
        public int MatchId { get; init; }
        public int WhiteOpponent { get; init; }
        public int BlackOpponent { get; init; }

    }
}
