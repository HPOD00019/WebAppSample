using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchMakingService.Domain.Entities;

namespace MatchMakingService.Domain.Repositories
{
    public interface IUserRepository
    {
        IResult<TimeSpan> ResetMatchRequestTTL(int userid);
        IResult<TimeSpan> AddUserToQueue(User user);
        Task<ICollection<User>> GetUsersWithRatingFromTo(int from, int to);
        Task<User> GetUserById(int id);
    }
}
