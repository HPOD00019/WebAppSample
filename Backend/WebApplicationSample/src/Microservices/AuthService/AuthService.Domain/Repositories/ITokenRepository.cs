using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Domain.Models;
using AuthService.Domain.Services;

namespace AuthService.Domain.Repositories
{
    public interface ITokenRepository
    {
        public Task<RefreshToken> RegisterRefreshToken(RefreshToken token);
        public Task <List<RefreshToken>> GetRefreshTokensByUserId(int Id);
        public Task<IResult<RefreshToken>> ValidateRefreshToken(string value);
    }
}
