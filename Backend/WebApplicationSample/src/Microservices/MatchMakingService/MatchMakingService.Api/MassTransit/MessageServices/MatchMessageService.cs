using MassTransit;
using MassTransit.Conracts.Commands;
using MatchMakingService.Application.Commands;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.Services;

namespace MatchMakingService.Api.MassTransit.MessageServices
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

        public async Task PublishNewMatchRequest(Match match)
        {
            var correlationId = NewId.NextGuid();
            var requestMatchCommand = new CreateNewMatchCommand
            {
                CorrelationId = correlationId,
                MatchId = match.Id,
                BlackOpponentId = match.Black,
                WhiteOpponentId = match.White,
                TimeStamp = DateTime.UtcNow,
                TimeControl = (int)match.control,
            };

            await _endpoint.Publish(requestMatchCommand, context =>
            {
                context.CorrelationId = correlationId;
                context.MessageId = NewId.NextGuid();
            });
        }
    }
}
