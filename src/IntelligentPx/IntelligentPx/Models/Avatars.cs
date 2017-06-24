using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntelligentPx.Models
{
    [JsonConverter(typeof(AvatarsConverter))]
    public class Avatars
    {
        [JsonProperty("default")]
        public string Default { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("tiny")]
        public string Tiny { get; set; }
    }

    internal class AvatarsConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Avatars);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            MoveHttpsUrl(obj, "default");
            MoveHttpsUrl(obj, "large");
            MoveHttpsUrl(obj, "small");
            MoveHttpsUrl(obj, "tiny");
            using (reader = obj.CreateReader())
            {
                existingValue = existingValue ?? new Avatars();
                serializer.Populate(reader, existingValue);
            }
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        private void MoveHttpsUrl(JObject rootObj, string childName)
        {
            var child = (JObject) rootObj[childName];
            rootObj.Remove(childName);
            rootObj.Add(childName, child["https"]);
        }
    }
}