using MediatR;
using AuthService.Application.Commands.Register;
using AuthService.Domain.Repositories;
using AuthService.Domain.Models;
using AuthService.Domain.PasswordSecurity;
using System.Windows.Markup;
namespace AuthService.Application.Handlers
{
    class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private IUserRepository _userRepository;
        private IPasswordHasher _passwordHashService;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHashService)
        {
            _passwordHashService = passwordHashService;
            _userRepository = userRepository;
        }



        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User();


            var plainPassword = request.Password;

            var salt = await _passwordHashService.GenerateSalt(16);
            var passwordHash = await _passwordHashService.HashPassword(plainPassword, salt);


            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = salt;


            var Id = await _userRepository.RegisterUser(user);
            return Id;
        }
    }
}
