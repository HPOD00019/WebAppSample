using MediatR;
using AuthService.Application.Commands.Register;
using AuthService.Domain.Repositories;
using AuthService.Domain.Models;

namespace AuthService.Application.Handlers
{
    class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private IUserRepository _userRepository;


        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User();

            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PasswordHash = request.Password;


            var Id = await _userRepository.RegisterUser(user);
            return Id;
        }
    }
}
