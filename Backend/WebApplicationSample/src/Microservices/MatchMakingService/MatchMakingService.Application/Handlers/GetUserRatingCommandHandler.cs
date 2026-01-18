using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchMakingService.Application.Commands;
using MatchMakingService.Application.Services;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.Repositories;
using MediatR;

namespace MatchMakingService.Application.Handlers
{
    public class GetUserRatingCommandHandler : IRequestHandler<GetUserRatingCommand, IResult<int>>
    {
        private IUserRepository _userRepository;

        public GetUserRatingCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IResult<int>> Handle(GetUserRatingCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user == null)
            {
                throw new NotImplementedException();
            }
            var rating = user.BlitzRating;
            
            return (Result<int>.OnSuccess(rating.Value));
        }
    }
}
