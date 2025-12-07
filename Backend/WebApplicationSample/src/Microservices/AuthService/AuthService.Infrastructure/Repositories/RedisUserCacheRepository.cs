using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AuthService.Application.Services;
using AuthService.Domain.Errors;
using AuthService.Domain.Models;
using AuthService.Domain.Repositories;
using AuthService.Domain.Services;
using AuthService.Infrastructure.Redis;
using StackExchange.Redis;

namespace AuthService.Infrastructure.Repositories
{
    public class RedisUserCacheRepository : IUserCacheRepository
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly IDatabase _db;

        public RedisUserCacheRepository(IConnectionMultiplexer connection)
        {
            _connection = connection;
            _db = _connection.GetDatabase();
        }
        public async Task<IResult<TimeSpan>> AddAccessToken(AccessToken token)
        {
            string key = RedisKeyGenerator.GenerateUserAccessTokenKeyById(token.IssuerId);
            string data = token.Value;
            await _db.StringSetAsync($"{key}", data, TimeSpan.FromDays(1));
            var result = Result<TimeSpan>.OnSuccess(TimeSpan.FromDays(1));
            return result;
        }

        public async Task<IResult<bool>> ApproveToken(AccessToken token)
        {
            string key = RedisKeyGenerator.GenerateUserAccessTokenKeyById(token.IssuerId);
            string? data = await _db.StringGetAsync(key);
            if(data == null)
            {
                var error = new Error(ErrorCode.AccessTokenInvalid, "No access token found for user");
                var result = Result<bool>.OnFailure(error);
                return result;
            }
            else
            {
                var result = Result<bool>.OnSuccess(true);
                return result;
            }
        }
    }
}
