using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MatchMakingService.Application.Commands;
using MatchMakingService.Application.Services;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.Repositories;
using MediatR;

namespace MatchMakingService.Application.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IResult<int>>
    {
        private IUserRepository _userRepository;
        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IResult<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.user;

            var isUserExists = await _userRepository.IsUserExists(user);
            if(isUserExists) { throw new Exception("User exists error in register user handler MMS"); }

            if (user.BlitzRating == null || user.BlitzRating <= 100) user.BlitzRating = 100;
            if (user.RapidRating == null || user.RapidRating <= 100) user.RapidRating = 100;
            if (user.ClassicalRating == null || user.ClassicalRating <= 100) user.ClassicalRating = 100;

            _userRepository.RegisterUser(user);
            var result = Result<int>.OnSuccess(user.Id);
            return result;
        }
    }
}
