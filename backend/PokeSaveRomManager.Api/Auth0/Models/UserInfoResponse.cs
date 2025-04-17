using System.Text.Json.Serialization;

namespace PokeSaveRomManager.Api.Auth0.Models
{
    public class UserInfoResponse
    {
        [JsonPropertyName("sub")]
        public string Sub { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("picture")]
        public string Picture { get; set; }
    }
}
