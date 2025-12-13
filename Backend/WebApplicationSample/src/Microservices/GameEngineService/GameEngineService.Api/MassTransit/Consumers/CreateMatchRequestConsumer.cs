using MassTransit;
using MassTransit.Conracts.Commands;
using MassTransit.Conracts.Events;
using MediatR;

namespace GameEngineService.Api.MassTransit.Consumers
{
    public class CreateMatchRequestConsumer : IConsumer<CreateNewMatchCommand>
    {
        private readonly IPublishEndpoint _endpoint;
        private readonly IMediator _mediator;

        public CreateMatchRequestConsumer(IPublishEndpoint endpoint, IMediator mediator)
        {
            _endpoint = endpoint;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateNewMatchCommand> context)
        {
            CreateNewMatchCommand c = context.Message;
            throw new NotImplementedException();
        }

    }
}
