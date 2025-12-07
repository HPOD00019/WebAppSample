using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Application.Commands.Register;
using AuthService.Application.Services;
using AuthService.Domain.Errors;
using AuthService.Domain.Models;
using AuthService.Domain.PasswordSecurity;
using AuthService.Domain.Repositories;
using AuthService.Domain.Services;
using MediatR;

namespace AuthService.Application.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, IResult<string>>
    {
        private IUserRepository _userRepository;
        private IPasswordHasher _passwordHasher;
        private ITokenService _tokenService;
        public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher hasher, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = hasher;
            _tokenService = tokenService;
        }

        public async Task<IResult<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByName(request.UserName);
            if (user == null)
            {
                var error = new Error(ErrorCode.UserNameInvalid);
                var _result = Result<string>.OnFailure(error);
                return _result;
            }
            var hash = user.PasswordHash;
            var salt = user.PasswordSalt;
            var plainPassword = request.Password;

            var newHash = await _passwordHasher.HashPassword(plainPassword, salt);
            if(newHash != hash)
            {
                var error = new Error(ErrorCode.PasswordInvalid);
                var _result = Result<string>.OnFailure(error);
                return _result;
            }

            var refreshTokenGenerationResult = await _tokenService.GenerateRefreshToken(user);
            return refreshTokenGenerationResult;
        }
    }
}
