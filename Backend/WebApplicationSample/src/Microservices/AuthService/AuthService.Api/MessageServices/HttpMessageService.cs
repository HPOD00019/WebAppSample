using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AuthService.Api.Urls;
using AuthService.Domain.Models;
using AuthService.Domain.Services;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuthService.Api.MessageServices
{
    public class HttpMessageService : IMessageService
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceSettings _settings;
        public HttpMessageService(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }
        public async Task SendRegisterUserEvent(User user)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                PropertyNameCaseInsensitive = false
            };

            var endpointAddress = _settings.MatchMakingServiceOnRegisterUserUrl;
            var json = JsonSerializer.Serialize(user, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpointAddress, content);
        }
    }
}
