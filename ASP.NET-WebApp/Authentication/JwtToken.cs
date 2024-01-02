using Newtonsoft.Json;

namespace ASP.NET_WebApp.Authentication
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("expiring_date")]
        public DateTime ExpiresAt { get; set; }
    }
}
