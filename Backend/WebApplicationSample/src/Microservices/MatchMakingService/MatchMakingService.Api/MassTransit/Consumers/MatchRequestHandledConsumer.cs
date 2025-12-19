using MassTransit;
using MassTransit.Conracts.Events;
using MatchMakingService.Application.Commands;
using MediatR;

namespace MatchMakingService.Api.MassTransit.Consumers
{
    public class MatchRequestHandledConsumer : IConsumer<MatchRequestHandledEvent>
    {
        private readonly IPublishEndpoint _endpoint;
        private readonly IMediator _mediator;

        public MatchRequestHandledConsumer(IPublishEndpoint endpoint, IMediator mediator)
        {
            _endpoint = endpoint;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<MatchRequestHandledEvent> context)
        {
            var message = context.Message;
            var command = new HandleMatchCreatedCommand
            {
                Id = message.MatchId,
                Link = message.JoinGameLink,
            };
            await _mediator.Send(command);

        }

    }
}
