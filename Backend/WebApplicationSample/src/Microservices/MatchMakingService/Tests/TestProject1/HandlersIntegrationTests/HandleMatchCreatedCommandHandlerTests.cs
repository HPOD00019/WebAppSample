using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Testcontainers.Redis;
using MatchMakingService.Application.Handlers;
using MatchMakingService.Infrastructure.Repositories;
using MatchMakingService.Domain.Repositories;
using MatchMakingService.Application.Commands;
using MatchMakingService.Infrastructure;

namespace TestProject1.HandlersIntegrationTests
{
    [TestFixture]
    public class HandleMatchCreatedCommandHandlerTests
    {
        private RedisContainer _redis;
        private IConnectionMultiplexer _multiplexer;
        private IDatabase _redisDB;
        private HandleMatchCreatedCommandHandler _commandHandler;
        private RedisUserRepository _userRepository;

        [OneTimeSetUp]
        public async Task OnInitalize()
        {
            _redis = new RedisBuilder().WithImage("redis:7-alpine").WithImage("redis:7-alpine").WithCleanUp(true).WithAutoRemove(true).Build();
            await _redis.StartAsync();
            var connectionString =$"{_redis.Hostname}:{_redis.GetMappedPublicPort(6379)},allowAdmin=true";
            _multiplexer = await ConnectionMultiplexer.ConnectAsync(connectionString);
            _redisDB = _multiplexer.GetDatabase();
            _userRepository = new RedisUserRepository(_multiplexer);
            _commandHandler = new HandleMatchCreatedCommandHandler(_userRepository);
        }
        [SetUp]
        public async Task SetUp()
        {
            var server = _multiplexer.GetServer(_redis.Hostname, _redis.GetMappedPublicPort(6379));
            await server.FlushAllDatabasesAsync();
        }

        [Test]
        public async Task StringSet_And_StringGet_Should_Work()
        {
            const string key = "test-key";
            const string expectedValue = "test-value";

            await _redisDB.StringSetAsync(key, expectedValue);
            var actualValue = await _redisDB.StringGetAsync(key);

            Assert.That(actualValue.ToString(), Is.EqualTo(expectedValue));
        }

        [Test] 
        public async Task Handle_Match_Created_with_empty_DB_Should_Return_Error_Match_Is_not_found()
        {
            var command = new HandleMatchCreatedCommand
            {
                Link = "SOME_TEST_LINK",
                Id = 1,
            };
            var result = await _commandHandler.Handle(command, new CancellationToken());
            Assert.That(result.IsSuccess == false);
            Assert.That(result.EmergedError.code == MatchMakingService.Domain.Entities.ErrorCode.MatchIsNotFound);
        }
        [Test]
        public async Task Handle_Match_Created_DB_ready_Should_Return_Success_and_Update_Db()
        {
            var matchId = 1;
            var key = RedisKeysGenerator.GetMatchKey(matchId);
            var whitePlayerId = 5;
            var blackPlayerId = 6;

            var value = $"{whitePlayerId}-{blackPlayerId}";
            await _redisDB.StringSetAsync(key, value);

            var command = new HandleMatchCreatedCommand
            {
                Link = "TEST_LINK",
                Id = matchId,
            };
            var result = await _commandHandler.Handle(command, new CancellationToken());

            var black_keyForLink = RedisKeysGenerator.GetMatchKeyForUser(blackPlayerId);
            var white_keyForLink = RedisKeysGenerator.GetMatchKeyForUser(whitePlayerId);

            var black_linkFromDB = await _redisDB.StringGetAsync(black_keyForLink);
            var white_linkFromDB = await _redisDB.StringGetAsync(white_keyForLink);
            
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(black_linkFromDB == "TEST_LINK");
            Assert.That(white_linkFromDB == "TEST_LINK");
        }

    }
}
