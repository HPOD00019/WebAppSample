
using AuthMiddleware.Middleware;
namespace MatchMakingService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();
            app.UseAuthentificationService();
            app.MapGet("/", () => "Hello World!");

            app.UseSwagger();
            app.UseSwaggerUI();
            app.Run();
        }
    }
}