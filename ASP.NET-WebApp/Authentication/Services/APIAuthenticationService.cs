using ASP.NET_Auth_under_the_hood_test.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace ASP.NET_WebApp.Authentication.Services
{
    public class APIAuthenticationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public APIAuthenticationService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<JwtToken> EnsureTokenAsync(bool forceRefresh = false)
        {
            string? token = _httpContextAccessor.HttpContext?.Session.GetString("access_token");
            JwtToken? jwtToken;

            if (string.IsNullOrWhiteSpace(token) || IsTokenExpired(token) || forceRefresh)
            {
                token = await ObtainNewTokenAsync();
                _httpContextAccessor.HttpContext?.Session.SetString("access_token", token);
                jwtToken = JsonConvert.DeserializeObject<JwtToken>(token);
                return jwtToken ?? new JwtToken();
            }
            else
            {
                return JsonConvert.DeserializeObject<JwtToken>(token) ?? new JwtToken();
            }
        }

        private static bool IsTokenExpired(string token)
        {
            var jwt = JsonConvert.DeserializeObject<JwtToken>(token);
            Console.WriteLine("Current UTC Time: " + DateTime.UtcNow);
            Console.WriteLine("JWT Expires At: " + jwt.ExpiresAt);

            bool isExpired = jwt?.ExpiresAt <= DateTime.UtcNow;

            Console.WriteLine("Is JWT Expired? " + isExpired);

            return jwt?.ExpiresAt <= DateTime.UtcNow;
        }

        private async Task<string> ObtainNewTokenAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("JWTApi");
            string authKey = _configuration.GetValue<string>("Authentication-Key") ?? "";

            httpClient.DefaultRequestHeaders.Add("Authentication-Key", authKey);
            var response = await httpClient.PostAsJsonAsync("auth", new UserDTO { UserID = Guid.NewGuid().ToString() });
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
