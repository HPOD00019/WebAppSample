using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Application.Commands;
using AuthService.Application.Services;
using AuthService.Domain.Repositories;
using AuthService.Domain.Services;
using MediatR;

namespace AuthService.Application.Handlers
{
    public class GetUserNameByIdCommandHandler : IRequestHandler<GetUserNameByIdCommand, IResult<string>>
    {
        private IUserRepository _userRepository;
        public GetUserNameByIdCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IResult<string>> Handle(GetUserNameByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user == null)
            {
                throw new NotImplementedException();
            }
            return Result<string>.OnSuccess(user.UserName);
        }
    }
}
