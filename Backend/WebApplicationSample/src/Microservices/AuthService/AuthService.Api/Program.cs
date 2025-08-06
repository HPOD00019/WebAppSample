using AuthService.Application.Commands.Register;
using AuthService.Infrastructure;
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
