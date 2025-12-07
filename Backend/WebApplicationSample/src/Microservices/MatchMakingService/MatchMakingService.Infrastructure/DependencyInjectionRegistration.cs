using MatchMakingService.Domain.Repositories;
using MatchMakingService.Infrastructure.DBcontext;
using MatchMakingService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MatchMakingService.Infrastructure
{
    public static class DependencyInjectionRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var redisConnectionString = config.GetConnectionString("Redis");
            var postgresqlConnectionString = config.GetConnectionString("Postgresql");
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(redisConnectionString));
            services.AddScoped<ICacheUserRepository, RedisUserRepository>();

            services.AddScoped<IUserRepository, PostgresqlUserRepository>();

            services.AddDbContext<MatchMakingMicroserviceDbContext>(options =>
            {
                options.UseNpgsql(
                    postgresqlConnectionString,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.MigrationsAssembly(typeof(MatchMakingMicroserviceDbContext).Assembly.FullName);
                    });
            });
            return services;
        }
    }
}
