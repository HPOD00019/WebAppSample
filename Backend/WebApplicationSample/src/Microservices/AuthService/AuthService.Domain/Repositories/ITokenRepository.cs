using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Domain.Models;

namespace AuthService.Domain.Repositories
{
    public interface ITokenRepository
    {
        public void RegisterRefreshToken(RefreshToken token);
    }
}
