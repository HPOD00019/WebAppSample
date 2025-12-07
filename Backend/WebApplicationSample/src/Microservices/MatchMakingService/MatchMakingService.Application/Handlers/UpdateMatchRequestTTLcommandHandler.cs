
using MatchMakingService.Application.Commands;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.Repositories;
using MediatR;

namespace MatchMakingService.Application.Handlers
{
    public class UpdateMatchRequestTTLcommandHandler : IRequestHandler<UpdateMatchRequestTTLcommand, IResult<TimeSpan>>
    {
        private ICacheUserRepository _userRepository;

        public UpdateMatchRequestTTLcommandHandler(ICacheUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IResult<TimeSpan>> Handle(UpdateMatchRequestTTLcommand request, CancellationToken cancellationToken)
        {
            var id = request.IssuerId;
            var result = await _userRepository.ResetMatchRequestTTL(id, request.control);
            return result;
        }
    }
}
