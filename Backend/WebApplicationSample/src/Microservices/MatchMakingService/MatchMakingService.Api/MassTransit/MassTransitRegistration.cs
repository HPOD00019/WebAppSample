using MassTransit;
using MassTransit.Conracts.Commands;
using MatchMakingService.Api.MassTransit.Configuration;
using MatchMakingService.Api.MassTransit.Consumers;
using MatchMakingService.Api.MassTransit.MessageServices;
using MatchMakingService.Domain.Services;

namespace MatchMakingService.Api.MassTransit
{
    public static class MassTransitRegistration
    {
        public static IServiceCollection RegisterMassTransitServices(this IServiceCollection services, IConfigurationSection settings)
        {
            services.AddScoped<IMatchMessageService, MatchMessageService>();
            var host = settings["Host"];
            var port = Int32.Parse(settings["Port"]);
            var virtualHost = settings["VirtualHost"];
            var username = settings["Username"];
            var password = settings["Password"];


            var prefetchCount = settings.GetValue("PrefetchCount", 16);
            var retryLimit = settings.GetValue("RetryLimit", 3);
            var retryInterval = settings.GetValue("RetryInterval", "00:00:05");
            var concurrentMessageLimit = settings.GetValue<int?>("ConcurrentMessageLimit");


            services.AddMassTransit(context =>
            {
                context.AddConsumers(typeof(MatchRequestHandledConsumer).Assembly);
                context.UsingRabbitMq((cnt, cfg) =>
                {
                    var rabbitMqUri = new Uri($"amqp://{username}:{password}@{host}:{port}/{virtualHost}");
                    cfg.Host(rabbitMqUri);
                    cfg.ConfigureEndpoints(cnt);

                    cfg.PrefetchCount = (ushort)prefetchCount;
                });
            });

            return services;
        }
    }
}
