using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakingService.Domain.Repositories
{
    public interface ICacheMatchRepository
    {
        Task CreateNewMatch(int matchId);
    }
}
