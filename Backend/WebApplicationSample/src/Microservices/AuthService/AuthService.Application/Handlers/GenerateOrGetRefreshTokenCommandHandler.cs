using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Application.Commands.GenerateToken;
using AuthService.Application.Services;
using AuthService.Domain.Models;
using AuthService.Domain.Repositories;
using AuthService.Domain.Services;
using MediatR;

namespace AuthService.Application.Handlers
{
    class GenerateOrGetRefreshTokenCommandHandler : IRequestHandler<GenerateOrGetRefreshTokenCommand, IResult<string>>
    {
        private ITokenService _tokenService;
        private ITokenRepository _tokenRepository;
        private IUserRepository _userRepository;
        public GenerateOrGetRefreshTokenCommandHandler(ITokenService tokenService, IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<IResult<string>> Handle(GenerateOrGetRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokens = await _tokenRepository.GetRefreshTokensByUserId(request.UserId);
            foreach (var token in tokens)
            {
                if (token.IsActive == true)
                {
                    var _result = Result<string>.OnSuccess(token.Value);
                    return _result;
                }
            }
            var user = await _userRepository.GetUserById(request.UserId);
            var result = await _tokenService.GenerateRefreshToken(user);
            if(result.IsSuccess == true)
            {
                var token = new RefreshToken
                {
                    Value = result.Value,
                    User = request.UserId,
                    ExpireAt = DateTime.UtcNow.AddDays(1),
                    IsActive = true
                };
                await _tokenRepository.RegisterRefreshToken(token);
            }
            return result;
        }
    }
}
