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
    internal class MatchFinishedCommandHandler : IRequestHandler<MatchFinishedCommand, IResult<bool>>
    {
        private ICacheUserRepository _cacheRepository;
        private IUserRepository _userRepository;
        public MatchFinishedCommandHandler(ICacheUserRepository cacheRepository, IUserRepository userRepository)
        {
            _cacheRepository = cacheRepository;
            _userRepository = userRepository;
        }
        public async Task<IResult<bool>> Handle(MatchFinishedCommand request, CancellationToken cancellationToken)
        {
            var playersRequestResult = await _cacheRepository.GetOpponentsByMatchId(request.matchId);
            if (playersRequestResult.IsSuccess == false)
            {
                var error = new Error(ErrorCode.MatchIsNotFound);
                var result = Result<bool>.OnFailure(error);
                return result;
            }
            var players = playersRequestResult.Value;
            var whiteId = players.Item1;
            var blackId = players.Item2;

            var white = await _userRepository.GetUserById(whiteId);
            var black = await _userRepository.GetUserById(blackId);

            if(black == null || white == null)
            {
                var error = new Error(ErrorCode.UserNotFoundInDataBase);
                var result = Result<bool>.OnFailure(error);
                return result;
            }

            await _cacheRepository.RemoveMatchForUser(whiteId);
            await _cacheRepository.RemoveMatchForUser(blackId);
            await _cacheRepository.RemoveMatch(request.matchId);

            if ((int)request.result <= 2)
            {
                var timeControl = request.control;

                var WhitesRating = white.GetRatingByTimeControl(timeControl);
                var BlackRating = black.GetRatingByTimeControl(timeControl);

                WhitesRating += 1;
                BlackRating -= 1;

                white.SetRatingByTimeControl(timeControl, WhitesRating);
                black.SetRatingByTimeControl(timeControl, BlackRating);

                await _userRepository.UpdateUser(black);
                await _userRepository.UpdateUser(white);

                var result = Result<bool>.OnSuccess(true);

                return result;
            }
            if ((int)request.result >= 5)
            {
                var timeControl = request.control;

                var WhitesRating = white.GetRatingByTimeControl(timeControl);
                var BlackRating = black.GetRatingByTimeControl(timeControl);

                WhitesRating -= 1;
                BlackRating += 1;

                white.SetRatingByTimeControl(timeControl, WhitesRating);
                black.SetRatingByTimeControl(timeControl, BlackRating);

                await _userRepository.UpdateUser(black);
                await _userRepository.UpdateUser(white);

                var result = Result<bool>.OnSuccess(true);

                return result;
            }
            throw new NotImplementedException();
        }
    }
}
