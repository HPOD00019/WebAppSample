using AuthService.Application.Commands.Register;
using MediatR;

namespace AuthService.Domain.Handlers
{
    class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        public RegisterUserCommandHandler()
        {

        }

        public Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
