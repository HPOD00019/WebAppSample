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
        Task<bool> IsUserExists(User user);
        void RegisterUser(User user);
        Task<User?> GetUserById(int id);
    }
}
