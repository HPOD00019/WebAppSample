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
    class GenerateOrGetAccessTokenCommandHandler : IRequestHandler<GenerateOrGetAccessTokenCommand, IResult<string>>
    {
        private ITokenService _tokenService;
        private ITokenRepository _tokenRepository;
        public GenerateOrGetAccessTokenCommandHandler(ITokenService tokenService, ITokenRepository tokenRepository)
        {
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
        }

        public async Task<IResult<string>> Handle(GenerateOrGetAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = request.RefreshToken;
            var tokenValidationResult = await _tokenRepository.ValidateRefreshToken(request.RefreshToken);
            if (tokenValidationResult.IsSuccess)
            {
                var userId = tokenValidationResult.Value.User;

            }
            var result = await _tokenService.GenerateAccessToken(request.RefreshToken);
            return result;
        }
    }
}
