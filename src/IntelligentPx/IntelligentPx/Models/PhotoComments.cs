using Newtonsoft.Json;

namespace IntelligentPx.Models
{
    public class PhotoComments
    {
        [JsonProperty("media_type")]
        public string MediaType { get; set; }
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
        [JsonProperty("total_items")]
        public int TotalItems { get; set; }
        [JsonProperty("comments")]
        public Comment[] Comments { get; set; }
    }
}