using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Services;
using GameEngineService.Infrastructure.Hubs;
using GameEngineService.Infrastructure.SignalRws;

namespace GameEngineService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<ISocketService<ChessGameMessage>, SignalRSocketService>();
            builder.Services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); 
                });
            });

            
            var app = builder.Build();
           
            app.UseCors("ReactApp");
            app.MapGet("/", () => "Hello World!");
            app.MapHub<PlayerHub>("/GoinGame");
            
            app.Run();
        }
    }
}
