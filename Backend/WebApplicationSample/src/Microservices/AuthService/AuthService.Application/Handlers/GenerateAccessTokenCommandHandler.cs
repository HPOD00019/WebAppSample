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
    class GenerateAccessTokenCommandHandler : IRequestHandler<GenerateAccessTokenCommand, IResult<string>>
    {
        private ITokenService _tokenService;
        public GenerateAccessTokenCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<IResult<string>> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _tokenService.GenerateAccessToken(request.RefreshToken);
            return result;
        }
    }
}
