using Newtonsoft.Json;

namespace IntelligentPx.Models
{
    public class Image
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("https_url")]
        public string HttpsUrl { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }
    }
}