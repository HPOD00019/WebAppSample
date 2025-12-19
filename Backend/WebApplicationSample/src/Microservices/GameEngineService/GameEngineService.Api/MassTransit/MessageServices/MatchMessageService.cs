using MassTransit;
using MassTransit.Conracts.Commands;
using GameEngineService.Application.Commands;
using GameEngineService.Domain.Entities;
using GameEngineService.Domain.Services;
using MassTransit.Conracts.Events;

namespace GameEngineService.Api.MassTransit.MessageServices
{
    public class MatchMessageService : IMatchMessageService
    {
        private IPublishEndpoint _endpoint;
        private ISendEndpointProvider _provider;
        private IRequestClient<CreateNewMatchCommand> _requestClient;
        public MatchMessageService(IPublishEndpoint endpoint, ISendEndpointProvider provider, IRequestClient<CreateNewMatchCommand> requestClient)
        {
            _endpoint = endpoint;
            _provider = provider;
            _requestClient = requestClient;
        }

        public async Task PublishMatchCreatedMessage(int matchId)
        {
            var correlationId = NewId.NextGuid();
            var matchCreatedEvent = new MatchRequestHandledEvent
            {
                CorrelationId = correlationId,
                MatchId = matchId,
                TimeStamp = DateTime.UtcNow,
                IsSuccess = true,
                JoinGameLink = Urls.Urls.JoinGameLink,
            };
            await _endpoint.Publish(matchCreatedEvent, context =>
            {
                context.CorrelationId = correlationId;
                context.MessageId = NewId.NextGuid();
               
            });
        }
    }
}
