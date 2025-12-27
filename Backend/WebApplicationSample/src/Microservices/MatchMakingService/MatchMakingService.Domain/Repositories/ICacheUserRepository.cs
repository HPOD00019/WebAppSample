using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.TimeControls;

namespace MatchMakingService.Domain.Repositories
{
    public interface ICacheUserRepository
    {
        Task RemoveMatchForUser(int userId);
        Task RemoveMatch(int matchId);
        Task<IResult<Tuple<int, int>>> GetOpponentsByMatchId (int matchId);
        Task CreateNewMatch(int matchId, int black, int white);
        Task RemoveUserFromSortedSet(int userId, TimeControl? control);
        Task SetMatchReady(int userId, string link);
        Task<Tuple<int, int>> GetMatchColors(int matchId);
        Task<IResult<string>> IsMatchReady(int userId);
        Task<IResult<TimeSpan>> ResetMatchRequestTTL(int userid, TimeControl? control = null);
        Task<IResult<TimeSpan>> AddUserToQueue(User user, TimeControl control);
        Task<ICollection<User>?> GetUsersWithRatingFromTo(int from, int to, TimeControl control);
        Task<TimeControl?> GetUserRequestTimeControlById(int id);
    }
}
