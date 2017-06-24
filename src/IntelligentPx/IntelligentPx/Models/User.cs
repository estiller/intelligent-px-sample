using Newtonsoft.Json;

namespace IntelligentPx.Models
{
    public class User
    {
        [JsonProperty("id")]
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

        [JsonProperty("usertype")]
        public int UserType { get; set; }

        [JsonProperty("fullname")]
        public string FullName { get; set; }

        [JsonProperty("userpic_url")]
        public string UserPicUrl { get; set; }

        [JsonProperty("userpic_https_url")]
        public string UserPicHttpsUrl { get; set; }

        [JsonProperty("cover_url")]
        public string CoverUrl { get; set; }

        [JsonProperty("upgrade_status")]
        public int UpgradeStatus { get; set; }

        [JsonProperty("store_on")]
        public bool StoreOn { get; set; }

        [JsonProperty("affection")]
        public int Affection { get; set; }

        [JsonProperty("avatars")]
        public Avatars Avatars { get; set; }
    }
}