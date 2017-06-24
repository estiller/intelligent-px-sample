using Newtonsoft.Json;

namespace IntelligentPx.Models
{
    public class PhotoCollection
    {
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_items")]
        public int TotalItems { get; set; }

        [JsonProperty("photos")]
        public Photo[] Photos { get; set; }

        [JsonProperty("feature")]
        public string Feature { get; set; }
    }
}