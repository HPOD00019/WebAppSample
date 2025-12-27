using MassTransit;
using MassTransit.Conracts.Events;
using MatchMakingService.Application.Commands;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.TimeControls;
using MediatR;

namespace MatchMakingService.Api.MassTransit.Consumers
{
    public class MatchFinishedConsumer : IConsumer<MatchFinishedEvent>
    {
        private readonly IMediator _mediator;
        public MatchFinishedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<MatchFinishedEvent> context)
        {
            var command = new MatchFinishedCommand
            {
                matchId = context.Message.MatchId,
                control = (TimeControl)context.Message.timeControl,
                result = (MatchResult)context.Message.matchResult,
            };
            var result = await _mediator.Send(command);
            if (result.IsSuccess == false) throw new NotImplementedException(result.EmergedError.code.ToString());

            
        }
    }
}
