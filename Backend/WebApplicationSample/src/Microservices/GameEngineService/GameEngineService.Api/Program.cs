using GameEngineService.Api.MassTransit;
using GameEngineService.Api.Urls;
using GameEngineService.Application;
using GameEngineService.Application.ChessCore;
using GameEngineService.Application.Commands;
using GameEngineService.Application.Connections;
using GameEngineService.Domain;
using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Services;
using GameEngineService.Infrastructure.Hubs;
using GameEngineService.Infrastructure.SignalRws;
using MediatR;
namespace GameEngineService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Urls.Urls.JoinGameLink = "http://localhost:5002/GoinGame";
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://localhost:5002");
            builder.Services.AddSingleton<ISocketService<ChessGameMessage>, SignalRSocketService>();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<SessionManager>();
            builder.Services.AddScoped<IChessCore, ChessCore>();
            builder.Services.AddScoped<IGameSession, WebSocketGameSession>();
            builder.Services.AddScoped<IGameConnection, WebSocketConnection>();
            builder.Services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            });
            builder.Services.RegisterMassTransitServices(builder.Configuration.GetSection("RabbitMQ"));
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

            builder.Services.AddMediatR(typeof(Program).Assembly, typeof(MatchCreateRequestCommand).Assembly);

            var app = builder.Build();
           
            app.UseCors("ReactApp");
            app.MapGet("/", () => "Hello World!");
            app.MapHub<PlayerHub>("/GoinGame");

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
        }
    }
}
