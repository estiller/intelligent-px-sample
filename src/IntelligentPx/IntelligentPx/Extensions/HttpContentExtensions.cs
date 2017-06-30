using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntelligentPx.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent httpContent)
        {
            var returnedValue = await httpContent.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(returnedValue);
        }
    }
}