using GameEngineService.Application;
using GameEngineService.Application.Commands;
using GameEngineService.Domain.Services;
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
        private IMatchMessageService _messageService;

        public CreateMatchRequestConsumer(IPublishEndpoint endpoint, IMediator mediator, IMatchMessageService messageService)
        {
            _messageService = messageService;
            _endpoint = endpoint;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateNewMatchCommand> context)
        {
            CreateNewMatchCommand c = context.Message;
            var command = new MatchCreateRequestCommand
            {
                Id = c.MatchId,
                Black = c.BlackOpponentId,
                White = c.WhiteOpponentId,
                Control = c.TimeControl,
            };
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                _messageService.PublishMatchCreatedMessage(c.MatchId);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

    }
}
