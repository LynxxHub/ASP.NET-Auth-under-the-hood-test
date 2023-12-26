using Newtonsoft.Json;

namespace ASP.NET_Auth_under_the_hood_test.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("expiring_date")]
        public DateTime ExpiresAt { get; set; }
    }
}
