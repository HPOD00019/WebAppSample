using MediatR;
using AuthService.Application.Commands.Register;
using AuthService.Domain.Repositories;
using AuthService.Domain.Models;
using AuthService.Domain.PasswordSecurity;
using AuthService.Domain.Services;
using AuthService.Application.Services;
using AuthService.Domain.Errors;

namespace AuthService.Application.Handlers
{
    class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IResult<int>>
    {
        private IUserRepository _userRepository;
        private IPasswordHasher _passwordHashService;
        private IMessageService _messageService;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHashService, IMessageService messageService)
        {
            _messageService = messageService;
            _passwordHashService = passwordHashService;
            _userRepository = userRepository;
        }

 

        public async Task<IResult<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User();


            var plainPassword = request.Password;

            var salt = await _passwordHashService.GenerateSalt(16);
            var passwordHash = await _passwordHashService.HashPassword(plainPassword, salt);

            var isUserNameExists = await _userRepository.IsUserNameExists(request.UserName);

            if (isUserNameExists)
            {
                var error = new Error(ErrorCode.UserNameExists);
                var _result = Result<int>.OnFailure(error);
                return _result;
            }

            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = salt;
            user.Role = UserRole.RegularUser;

            var Id = await _userRepository.RegisterUser(user);
            user.Id = Id;
            var result = Result<int>.OnSuccess(Id);
            _messageService.SendRegisterUserEvent(user);
            return result;
        }
    }
}
