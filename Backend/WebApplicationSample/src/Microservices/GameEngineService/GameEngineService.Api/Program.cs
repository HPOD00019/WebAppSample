using GameEngineService.Application;
using GameEngineService.Application.ChessCore;
using GameEngineService.Application.Connections;
using GameEngineService.Domain.Chess;
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
            builder.Services.AddSingleton<ISocketService<ChessGameMessage>, SignalRSocketService>();
            builder.Services.AddSingleton<IGameConnection, WebSocketConnection>();
            builder.Services.AddSingleton<IChessCore, ChessCore>();
            builder.Services.AddSingleton<IGameSession, WebSocketGameSession>();
            builder.Services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173", "http://localhost:5174")
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
