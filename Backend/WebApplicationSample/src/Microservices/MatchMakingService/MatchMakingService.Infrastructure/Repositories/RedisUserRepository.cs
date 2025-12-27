
using System;
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

        public async Task CreateNewMatch(int matchId, int blackOpponentId, int whiteOpponentId)
        {
            var key = RedisKeysGenerator.GetMatchKey(matchId);
            _db.StringSet(key, $"{whiteOpponentId}-{blackOpponentId}");
            
        }

        public async Task<Tuple<int, int>> GetMatchColors(int matchId)
        {
            var key = RedisKeysGenerator.GetMatchKey(matchId);
            string sides = await _db.StringGetAsync(key);
            var n = sides.Split('-');
            var white = Int32.Parse(n[0]);
            var black = Int32.Parse(n[1]);
            var ans = new Tuple<int, int>(white, black);

            return ans;
        }

        public async Task<IResult<Tuple<int, int>>> GetOpponentsByMatchId(int matchId)
        {
            var key = RedisKeysGenerator.GetMatchKey(matchId);
            string opponents = await _db.StringGetAsync(key);

            if (opponents == null)
            {
                var error = new Error(ErrorCode.MatchIsNotFound);
                var _result = Result<Tuple<int, int>>.OnFailure(error);
                return _result;
            }

            var parts = opponents.Split('-');
            int whiteOpponentId = int.Parse(parts[0]);
            int blackOpponentId = int.Parse(parts[1]);
            var p = new Tuple<int, int>(whiteOpponentId, blackOpponentId);
            var result = Result<Tuple<int, int>>.OnSuccess(p);
            return result;
        }

        public async Task<TimeControl?> GetUserRequestTimeControlById(int id)
        {
            string key = RedisKeysGenerator.GetUserKey(id);

            var timeControl = await _db.StringGetAsync(key);
            if (Enum.TryParse<TimeControl>(timeControl, out TimeControl result))
            {
                return result;
            }
            else
            {
                return null;
            }
            
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

        public async Task<IResult<string>> IsMatchReady(int userId)
        {
            var key = RedisKeysGenerator.GetMatchKeyForUser(userId);
            string matchLink = await _db.StringGetAsync(key);
            if (matchLink == null)
            {
                var error = new Error(ErrorCode.MatchIsNotFound);
                return Result<string>.OnFailure(error);
            }
            var result = Result<string>.OnSuccess(matchLink);
            return result;
        }

        public async Task RemoveMatch(int matchId)
        {
            var key = RedisKeysGenerator.GetMatchKey(matchId);
            _db.KeyDelete(key);
        }

        public async Task RemoveMatchForUser(int userId)
        {
            var key = RedisKeysGenerator.GetMatchKeyForUser(userId);
            _db.KeyDelete(key);
            
        }

        public async Task RemoveUserFromSortedSet(int userId, TimeControl? control)
        {
            if (control == null) throw new NotImplementedException();

            var key = RedisKeysGenerator.GetUserKey(userId);
            await _db.SortedSetRemoveAsync(control.ToString(), key);
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

        public async Task SetMatchReady(int userId, string link)
        {
            var key = RedisKeysGenerator.GetMatchKeyForUser(userId);
            _db.StringSet(key, link);
        }
    }
}
