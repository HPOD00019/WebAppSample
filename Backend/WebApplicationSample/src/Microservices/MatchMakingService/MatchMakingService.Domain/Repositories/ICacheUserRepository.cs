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
        Task<IResult<TimeSpan>> ResetMatchRequestTTL(int userid, TimeControl? control = null);
        Task<IResult<TimeSpan>> AddUserToQueue(User user, TimeControl control);
        Task<ICollection<User>?> GetUsersWithRatingFromTo(int from, int to, TimeControl control);
        Task<bool> GetUserById(int id, TimeControl? control);
    }
}
