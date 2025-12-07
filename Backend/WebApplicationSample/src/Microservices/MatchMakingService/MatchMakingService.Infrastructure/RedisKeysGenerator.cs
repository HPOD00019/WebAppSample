using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchMakingService.Domain;
using MatchMakingService.Domain.Entities;
using StackExchange.Redis;

namespace MatchMakingService.Infrastructure
{
    public static class RedisKeysGenerator
    {
        public const string User = "user";
        public static string GetUserKey(User user)
        {
            string id = user.Id.ToString();
            string key = $"{RedisKeysGenerator.User}:{id}";
            return key;
        }
        public static string GetUserKey(int id)
        {
            string key = $"{RedisKeysGenerator.User}:{id}";
            return key;
        }
        public static int GetIdByUserKey(string key)
        {
            int id = int.Parse(key.Split(':')[1]);
            return id;
        }
    }
}
