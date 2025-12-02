using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Application.Queries;
using AuthService.Application.Services;
using AuthService.Domain.Errors;
using AuthService.Domain.Models;
using AuthService.Domain.Repositories;
using AuthService.Domain.Services;
using MediatR;

namespace AuthService.Application.Handlers
{
    public class GetUserAsPlayerQueryHandler : IRequestHandler<GetUserAsPlayerQuery, IResult<User>>
    {
        private IUserRepository _userRepository;
        public GetUserAsPlayerQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IResult<User>> Handle(GetUserAsPlayerQuery request, CancellationToken cancellationToken)
        {
            var userId = request.userId;
            var user = await _userRepository.GetUserById(userId);
            if(user == null)
            {
                var error = new Error(ErrorCode.NoUserFound);
                var result = Result<User>.OnFailure(error);
                return result;
            }
            else
            {
                var result = Result<User>.OnSuccess(user);
                return result;
            }

        }
    }
}
