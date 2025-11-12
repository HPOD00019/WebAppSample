using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Application.Commands.GenerateToken;
using AuthService.Domain.Repositories;
using AuthService.Domain.Services;
using MediatR;

namespace AuthService.Application.Handlers
{
    class GenerateRefreshTokenCommandHandler : IRequestHandler<GenerateRefreshTokenCommand, IResult<string>>
    {
        private ITokenService _tokenService;
        private IUserRepository _userRepository;
        public GenerateRefreshTokenCommandHandler(ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<IResult<string>> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);
            var result = await _tokenService.GenerateRefreshToken(user);
            return result;
        }
    }
}
