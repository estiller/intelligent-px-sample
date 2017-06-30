using Newtonsoft.Json;

namespace IntelligentPx.Models
{
    public class ShortUser
    {
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("fullname")]
        public string FullName { get; set; }
        [JsonProperty("userpic_url")]
        public string UserpicUrl { get; set; }
        [JsonProperty("upgrade_status")]
        public int UpgradeStatus { get; set; }
    }
}