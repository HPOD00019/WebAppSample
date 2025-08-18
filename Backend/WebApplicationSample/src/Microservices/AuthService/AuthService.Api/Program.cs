using AuthService.Application.Commands.Register;
using AuthService.Domain.Services;
using AuthService.Infrastructure;
using AuthService.Application.Services;
using MediatR;


namespace AuthService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<ITokenService>(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                var privateKey = config["RSAencryptionKeys:PrivateKey"];
                var publicKey = config["RSAencryptionKeys:PublicKey"];

                var tokenService = new TokenService(privateKey, publicKey);
                return tokenService;

            });


            builder.Services.AddTransient<IMediator, Mediator>();
            builder.Services.AddMediatR(typeof(Program).Assembly, typeof(RegisterUserCommand).Assembly);

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();
            app.Run();
        }
    }
}
