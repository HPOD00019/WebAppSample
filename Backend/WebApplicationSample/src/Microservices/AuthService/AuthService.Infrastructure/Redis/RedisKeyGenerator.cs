using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Redis
{
    public static class RedisKeyGenerator
    {
        public const string User = "user";
        public const string AccessToken = "accesstoken";
        public static string GenerateUserAccessTokenKeyById(int userId)
        {
            string key = $"{RedisKeyGenerator.User}{RedisKeyGenerator.AccessToken}:{userId}";
            return key;
        }
    }
}
