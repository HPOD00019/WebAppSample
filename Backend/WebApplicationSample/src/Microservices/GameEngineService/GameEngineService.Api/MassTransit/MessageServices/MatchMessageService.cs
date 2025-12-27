using MassTransit;
using MassTransit.Conracts.Commands;
using GameEngineService.Application.Commands;
using GameEngineService.Domain.Entities;
using GameEngineService.Domain.Services;
using MassTransit.Conracts.Events;
using GameEngineService.Domain.TimeControls;

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

        public async Task PublishMatchFinishedMessage(MatchResultDTO results)
        {
            var correlationId = NewId.NextGuid();
            var matchFinishedEvent = new MatchFinishedEvent
            {
                CorrelationId = correlationId,
                MatchId = results.matchId,
                TimeStamp = DateTime.UtcNow,
                matchResult = (int)results.result,
                timeControl = (int)results.control,
            };
            await _endpoint.Publish(matchFinishedEvent, context =>
            {
                context.CorrelationId = correlationId;
                context.MessageId = NewId.NextGuid();
            });
        }
    }
}
