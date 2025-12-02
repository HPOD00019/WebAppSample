using System;
using System.Collections.Generic;
using System.Linq;
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
        private IUserRepository _userRepository;
        public CreateMatchRequestCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IResult<TimeSpan>> Handle(CreateMatchRequestCommand request, CancellationToken cancellationToken)
        {
            var result = _userRepository.AddUserToQueue(request.Issuer);
            return result;
            
        }
    }
}
