using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthMiddleware.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AuthMiddleware.Middleware
{
    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthentificationService(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
        public static IServiceCollection AddAuthService(this IServiceCollection services, string authServiceBaseUrl)
        {
            services.AddScoped<AuthService>(provider =>
            {
                var client = provider.GetRequiredService<HttpClient>();
                return new AuthService(client, authServiceBaseUrl);
            });
            
            return services;
        }
    }
}
