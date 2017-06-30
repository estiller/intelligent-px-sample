using Newtonsoft.Json;

namespace IntelligentPx.Models
{
    public class Comment
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("to_whom_user_id")]
        public int ToWhomUserId { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }
        [JsonProperty("user")]
        public ShortUser User { get; set; }
    }
}