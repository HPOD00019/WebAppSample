using AuthMiddleware.Entities;
using System.Text.Json;
using MatchMakingService.Api.UrlSettings;
using MatchMakingService.Domain.Entities;
using Microsoft.Extensions.Options;
using MatchMakingService.Api.DTOs;

namespace MatchMakingService.Api.HttpClientsServices
{
    public class UserServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceSettings _settings;
        public UserServiceClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }

        public async Task<User> GetUser(int id)
        {
            var endpointAddress = _settings.AuthServiceGetUserById;
            var requestUrl = $"{endpointAddress}?id={id}";
            var response = await _httpClient.GetAsync(requestUrl);
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var ans = JsonSerializer.Deserialize<ApiResponse>(content, options);
            if(ans.Success == true)
            {
                var user = JsonSerializer.Deserialize<User>(JsonSerializer.Serialize(ans.Data), options);
                return user;
            }
            else
            {
                throw new Exception(ans.ErrorCode);
            }

        }
    }
}
