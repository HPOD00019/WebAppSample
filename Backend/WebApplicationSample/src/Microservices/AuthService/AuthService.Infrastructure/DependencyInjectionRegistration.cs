using AuthService.Domain.Repositories;
using AuthService.Infrastructure.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Infrastructure.Repositories;
using AuthService.Domain.PasswordSecurity;
using AuthService.Infrastructure.PasswordHashServices;


namespace AuthService.Infrastructure
{
    public static class DependencyInjectionRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<AuthMicroserviceDbContext>(options =>
            {
                options.UseNpgsql(
                    config.GetConnectionString("AuthServiceDataBase"),
                    npgsqlOptions =>
                    {
                        npgsqlOptions.MigrationsAssembly(typeof(AuthMicroserviceDbContext).Assembly.FullName);
                    });
            });

            services.AddScoped<IPasswordHasher, ArgonHashService>();
            
            return services;
        }
    }
}
