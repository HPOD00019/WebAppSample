using MassTransit;
using MatchMakingService.Api.MassTransit.Messages.Commands;

namespace MatchMakingService.Api.MassTransit.Saga
{
    public class CreatingMatchSagaStateMachine : MassTransitStateMachine<CreatingMatchSagaState>
    {
        public State Initial { get; private set; }
        public State CreatingMatch { get; private set; }
        public State Cancelled { get; private set; }

        
    }
}
