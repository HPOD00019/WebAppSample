using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MatchMakingService.Application.Commands;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.Repositories;
using MediatR;

namespace MatchMakingService.Application.Handlers
{
    public class CreateMatchRequestCommandHandler : IRequestHandler<CreateMatchRequestCommand, IResult<TimeSpan>>
    {
        private ICacheUserRepository _userRepository;
        private IUserRepository _userDBrepository;
        public CreateMatchRequestCommandHandler(ICacheUserRepository userRepository, IUserRepository repository)
        {
            _userRepository = userRepository;
            _userDBrepository = repository;
        }
        public async Task<IResult<TimeSpan>> Handle(CreateMatchRequestCommand request, CancellationToken cancellationToken)
        {
            var _dbUser = await _userDBrepository.GetUserById(request.Issuer.Id);
            if (_dbUser == null) { throw new Exception("User was null at Create match handler"); }
            if (request.FromToSettings != null) throw new NotImplementedException("Not implemented castom rating settings");
            var userRating = _dbUser.GetRatingByTimeControl(request.control);

            var result = await _userRepository.AddUserToQueue(_dbUser, request.control);
            return result;
            
        }
    }
}
