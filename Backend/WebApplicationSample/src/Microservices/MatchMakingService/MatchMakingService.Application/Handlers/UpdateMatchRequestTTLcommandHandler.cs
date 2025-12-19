
using MatchMakingService.Application.Commands;
using MatchMakingService.Application.Services;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.Repositories;
using MatchMakingService.Domain.Services;
using MediatR;

namespace MatchMakingService.Application.Handlers
{
    public class UpdateMatchRequestTTLcommandHandler : IRequestHandler<UpdateMatchRequestTTLcommand, IResult<string>>
    {
        private ICacheUserRepository _userCacheRepository;
        private IUserRepository _userRepository;
        private IMatchMessageService _messageService;
        public UpdateMatchRequestTTLcommandHandler(ICacheUserRepository userCacheRepository, IUserRepository userRepository, IMatchMessageService messageService)
        {
            _userRepository = userRepository;
            _userCacheRepository = userCacheRepository;
            _messageService = messageService;
        }

        public async Task<IResult<string>> Handle(UpdateMatchRequestTTLcommand request, CancellationToken cancellationToken)
        {
            var id = request.IssuerId;
            var user = await _userRepository.GetUserById(id);
            var userRequestTimeControl = await _userCacheRepository.GetUserRequestTimeControlById(id);

            var matchLink = await _userCacheRepository.IsMatchReady(id);
            if (matchLink.IsSuccess)
            {
                var _result = Result<string>.OnSuccess(matchLink.Value);
                return _result;
            }
            {
                if (user == null)
                {
                    var error = new Error(ErrorCode.UserNotFoundInDataBase);
                    var _result = Result<string>.OnFailure(error);
                    return _result;
                }
                if (userRequestTimeControl != request.control || userRequestTimeControl == null)
                {
                    var error = new Error(ErrorCode.UpdateTTLtimeControlWasInvalid);
                    var _result = Result<string>.OnFailure(error);
                    return _result;
                }
            }
            var result = await _userCacheRepository.ResetMatchRequestTTL(id, request.control);
            if (result.IsSuccess == false)
            {
                var error = result.EmergedError;
                var _result = Result<string>.OnFailure(error);
                return _result;
            }
            
            
            var rating = user.GetRatingByTimeControl(userRequestTimeControl.Value);
            var users = await _userCacheRepository.GetUsersWithRatingFromTo((int)0.8*rating, (int)1.2*rating, userRequestTimeControl.Value);
            var opponent = users.FirstOrDefault(u => u.Id != id);
            if(opponent == null)
            {
                var error = new Error(ErrorCode.MatchIsNotFound);
                var _result = Result<string>.OnFailure(error);
                return _result;
            }
            else
            {
                await _userCacheRepository.RemoveUserFromSortedSet(opponent.Id, userRequestTimeControl);
                await _userCacheRepository.RemoveUserFromSortedSet(user.Id, userRequestTimeControl);
                
                Random random = new Random();
                int randomValue = random.Next(0, int.MaxValue);
                await _userCacheRepository.CreateNewMatch(randomValue, opponent.Id, user.Id);
                var match = new Match
                {
                    Id = randomValue,
                    Black = opponent.Id,
                    White = user.Id,
                    control = userRequestTimeControl.Value,
                };
                _messageService.PublishNewMatchRequest(match);
                var error = new Error(ErrorCode.MatchIsNotFound);
                var _result = Result<string>.OnFailure(error);

                return _result;
            }
        }
    }
}
