
using System.Text.Json;
using AuthMiddleware.Middleware;
using MatchMakingService.Api.HttpClientsServices;
using MatchMakingService.Api.UrlSettings;
using MatchMakingService.Application.Commands;
using MatchMakingService.Infrastructure;
using MediatR;
using Microsoft.OpenApi.Models;
namespace MatchMakingService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://localhost:5003");
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

            }); ;
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MatchMaking Service API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServicesUrls"));
            builder.Services.AddTransient<UserServiceClient>();
            builder.Services.AddHttpClient();
            builder.Services.AddAuthService("http://localhost:5001/Auth/verifyAccessToken?token=");
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddTransient<IMediator, Mediator>();
            builder.Services.AddMediatR(typeof(Program).Assembly, typeof(CreateMatchRequestCommand).Assembly);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            var app = builder.Build();

            app.UseCors("AllowAll");

            app.UseSwagger();
            app.UseSwaggerUI();
            
            app.UseAuthentificationService();
            app.MapGet("/", () => "Hello World!");

            app.MapControllers();
            app.Run();
        }
    }
}