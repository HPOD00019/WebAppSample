using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Application.Commands.TokenValidation;
using AuthService.Domain.Models;
using AuthService.Domain.Services;
using MediatR;
using Microsoft.VisualBasic;

namespace AuthService.Application.Handlers
{
    public class AccessTokenValidateCommandHandler : IRequestHandler<AccessTokenValidateCommand, IResult<User>>
    {
        private ITokenService _tokenService;
        

        public AccessTokenValidateCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<IResult<User>> Handle(AccessTokenValidateCommand request, CancellationToken cancellationToken)
        {
            var validationResult =await  _tokenService.ValidateAccessToken(request.AccessToken);
            return validationResult;
            
        }
    }
}
