using AuthService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Services
{
    public interface ITokenService
    {
        public Task<string> GenerateRefreshToken(User user);
        public Task<string> GenerateAccessToken(string refreshToken);

    }
}
