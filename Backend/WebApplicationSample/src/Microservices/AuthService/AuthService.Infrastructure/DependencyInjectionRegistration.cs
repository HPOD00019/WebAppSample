using AuthService.Domain.Repositories;
using AuthService.Infrastructure.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace AuthService.Infrastructure
{
    public static class DependencyInjectionRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserRepository, IUserRepository>();

            services.AddDbContext<AuthMicroserviceDbContext>(options =>
            {

                options.UseNpgsql(
                    config.GetConnectionString("AuthServiceDataBase")
                    );
            });


            return services;
        }
    }
}
