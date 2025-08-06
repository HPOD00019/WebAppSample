using AuthService.Domain.PasswordSecurity;
using Konscious.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;



namespace AuthService.Infrastructure.PasswordHashServices
{
    class ArgonHashService : IPasswordHasher
    {
        public async Task<string> GenerateSalt(int byteLen)
        {
            string salt = await Task.Run(() =>
            {
                var salt = new byte[byteLen];

                using (var NumberGenerator = RandomNumberGenerator.Create())
                {
                    NumberGenerator.GetBytes(salt);
                }

                string ans = Convert.ToBase64String(salt);
                return ans;
            });

            return salt;
        }

        public async Task<string> HashPassword(string PlainPassword, string Salt)
        {
            var HashedPassword = await Task.Run(() => 
            {
                var argon = new Argon2id(Encoding.UTF8.GetBytes(PlainPassword));

                argon.Salt = Encoding.UTF8.GetBytes(Salt);
                argon.DegreeOfParallelism = 1;
                argon.MemorySize = 65536;
                argon.Iterations = 3;

                return argon.GetBytes(32);
            });

            var Password = Convert.ToBase64String(HashedPassword);

            return Password;
        }
    }
}
