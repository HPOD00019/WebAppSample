using AuthService.Application.Commands.Register;
using AuthService.Domain.Services;
using AuthService.Infrastructure;
using AuthService.Application.Services;
using MediatR;
using AuthService.Api.Urls;
using AuthService.Api.MessageServices;


namespace AuthService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://localhost:5001");

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                }); 
                options.AddPolicy("WithCredentials", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowCredentials()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<ITokenService>(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                var privateKey = config["RSAencryptionKeys:PrivateKey"];
                var publicKey = config["RSAencryptionKeys:PublicKey"];

                var tokenService = new TokenService(privateKey, publicKey);
                return tokenService;

            });

            builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("Services"));
            builder.Services.AddTransient<IMessageService, HttpMessageService>();
            builder.Services.AddHttpClient();
            
            builder.Services.AddTransient<IMediator, Mediator>();
            builder.Services.AddMediatR(typeof(Program).Assembly, typeof(RegisterUserCommand).Assembly);

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();
            app.Run();
        }
    }
}
