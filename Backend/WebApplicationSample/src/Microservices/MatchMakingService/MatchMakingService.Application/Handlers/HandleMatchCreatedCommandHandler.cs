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
    public class HandleMatchCreatedCommandHandler : IRequestHandler<HandleMatchCreatedCommand, IResult<bool>>
    {
        private ICacheUserRepository _userRepository;
        
        public HandleMatchCreatedCommandHandler(ICacheUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IResult<bool>> Handle(HandleMatchCreatedCommand request, CancellationToken cancellationToken)
        {
            var opponentsResult = await _userRepository.GetOpponentsByMatchId(request.Id);
            if (opponentsResult.IsSuccess == false) throw new NotImplementedException();
            var opponents = opponentsResult.Value;
            
            var whiteId = opponents.Item1;
            var blackId = opponents.Item2;

            await _userRepository.SetMatchReady(whiteId, request.Link);
            await _userRepository.SetMatchReady(blackId, request.Link);

            var result = Result<bool>.OnSuccess(true);
            return result;
            
        }
    }
}
