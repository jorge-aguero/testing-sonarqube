using System.Text.Json.Serialization;

namespace SecurityHelper.Jwt
{
    public class JwtToken
    {
        [JsonPropertyName("aud")]
        public string? Audience { get; set; }
    }
}
