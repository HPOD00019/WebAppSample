
using System.Text.Json;
using MatchMakingService.Application.Services;
using MatchMakingService.Domain;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.Repositories;
using MatchMakingService.Domain.TimeControls;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;

namespace MatchMakingService.Infrastructure.Repositories
{
    public class RedisUserRepository : ICacheUserRepository
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly IDatabase _db;

        public RedisUserRepository(IConnectionMultiplexer multiplexer)
        {
            _connection = multiplexer;
            _db = _connection.GetDatabase();


        }

        public async Task<IResult<TimeSpan>> AddUserToQueue(User user, TimeControl control)
        {
            int id = user.Id;
            string key = RedisKeysGenerator.GetUserKey(id);
            var rating = user.GetRatingByTimeControl(control);
            await _db.SortedSetAddAsync(control.ToString(), key, rating);
            _db.StringSet(key, control.ToString(), TimeSpan.FromSeconds(260));
            var result = Result<TimeSpan>.OnSuccess(TimeSpan.FromMinutes(1));
            return result;
        }

        public async Task<bool> GetUserById(int id, TimeControl? control)
        {
            if (control == null) throw new NotImplementedException();


            string key = RedisKeysGenerator.GetUserKey(id);

            var user = await _db.StringGetAsync(key);

            if (user.IsNull) return false;
            return true;
            
        }

        public async Task<ICollection<User>?> GetUsersWithRatingFromTo(int from, int to, TimeControl control)
        {
            var result = await _db.SortedSetRangeByScoreWithScoresAsync(control.ToString(), from, to);
            var list = result.Select(e => new Tuple<string, int>(e.Element, (int)e.Score)).ToList();
            var users = new List<User>();
            foreach(var item in list)
            {
                var id = RedisKeysGenerator.GetIdByUserKey(item.Item1);
                var key = RedisKeysGenerator.GetUserKey(id);
                string _control = await _db.StringGetAsync(key);
                if (_control == null)
                {
                    await _db.SortedSetRemoveAsync(control.ToString(), key);
                    continue;
                }

                var user = new User
                {
                    Id = id,
                };
                user.SetRatingByTimeControl(control, item.Item2);
                users.Add(user);
            }
            return users;
        }

        public async Task<IResult<TimeSpan>> ResetMatchRequestTTL(int userid, TimeControl? control = null)
        {
            var userKey = RedisKeysGenerator.GetUserKey(userid);
            string _control = await _db.StringGetAsync(userKey);
            
            if(_control == null || control.ToString() != _control)
            {
                var error = new Error(ErrorCode.UserNotFoundInMatchRequests);
                var _result = Result<TimeSpan>.OnFailure(error);
                return _result;
            }
            _db.StringSet(userKey, control.ToString(), TimeSpan.FromSeconds(260));
            
            var result = Result<TimeSpan>.OnSuccess(TimeSpan.FromSeconds(60));
            return result;
        }

    }
}
