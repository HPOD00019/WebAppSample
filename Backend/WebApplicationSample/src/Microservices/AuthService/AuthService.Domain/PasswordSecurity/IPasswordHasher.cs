using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.PasswordSecurity
{
    public interface IPasswordHasher
    {
        public Task<string> HashPassword(string PlainPassword, string salt);
        public Task<string> GenerateSalt(int byteLen);
    }
}
