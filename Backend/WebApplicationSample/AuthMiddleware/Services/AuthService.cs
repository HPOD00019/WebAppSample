
using System.Text.Json;
using AuthMiddleware.Entities;

namespace AuthMiddleware.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _authServiceTokenVerifyUrl;
        public AuthService(HttpClient httpClient, string authServiceTokenVerifyUrl)
        {
            _httpClient = httpClient;
            _authServiceTokenVerifyUrl = authServiceTokenVerifyUrl;
        }
        public async Task<AuthResult> ValidateAccessToken(string accessToken)
        {
            var url = $"{_authServiceTokenVerifyUrl}{accessToken}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var ans = JsonSerializer.Deserialize<AuthResult>(content, options);
            return ans;
        } 

    }
}
