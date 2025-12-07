using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Domain.Models;
using AuthService.Domain.Services;

namespace AuthService.Domain.Repositories
{
    public interface IUserCacheRepository
    {
        Task<IResult<TimeSpan>> AddAccessToken(AccessToken token);
        Task<IResult<bool>> ApproveToken(AccessToken token);
        
    }
}
