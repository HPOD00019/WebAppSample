using MassTransit;
using GameEngineService.Api.MassTransit.Messages.Commands;

namespace GameEngineService.Api.MassTransit.Saga
{
    public class CreatingMatchSagaStateMachine : MassTransitStateMachine<CreatingMatchSagaState>
    {
        public State Initial { get; private set; }
        public State CreatingMatch { get; private set; }
        public State Cancelled { get; private set; }

        
    }
}
