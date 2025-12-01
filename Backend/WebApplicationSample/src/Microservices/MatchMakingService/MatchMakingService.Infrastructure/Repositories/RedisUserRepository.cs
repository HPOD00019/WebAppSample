
using System.Text.Json;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.Repositories;
using StackExchange.Redis;

namespace MatchMakingService.Infrastructure.Repositories
{
    public class RedisUserRepository : IUserRepository
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly IDatabase _db;

        public RedisUserRepository(IConnectionMultiplexer multiplexer)
        {
            _connection = multiplexer;
            _db = _connection.GetDatabase();
        }

        public void AddUserToQueue(User user)
        {
            int id = user.Id;
            string key = RedisKeysGenerator.GetUserKey(id);
            string userData = JsonSerializer.Serialize(user);
            _db.StringSetAsync($"{key}", userData, TimeSpan.FromMinutes(1));
        }

        public async Task<User> GetUserById(int id)
        {
            string key = RedisKeysGenerator.GetUserKey(id);
            var user = await _db.StringGetAsync(key);

            if (user.IsNull) throw new NotImplementedException("No such User");


            var _user = JsonSerializer.Deserialize<User>((string)user);
            return _user;
            
        }
        
        public Task<ICollection<User>> GetUsersWithRatingFromTo(int from, int to)
        {

            throw new NotImplementedException();
        }

        public void ResetMatchRequestTTL(int userid)
        {
            string key = RedisKeysGenerator.GetUserKey(userid);
            _db.KeyExpire(key, TimeSpan.FromMinutes(1));
        }
    }
}
