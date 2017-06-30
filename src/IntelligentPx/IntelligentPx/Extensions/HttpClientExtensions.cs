using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntelligentPx.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T content)
        {
            var serializedContent = JsonConvert.SerializeObject(content);
            return httpClient.PostAsync(requestUri, new StringContent(serializedContent, Encoding.UTF8, "application/json"));
        }
    }
}