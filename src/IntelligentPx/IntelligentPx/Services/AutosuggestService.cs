using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IntelligentPx.Extensions;
using Newtonsoft.Json.Linq;
using PCLAppConfig;

namespace IntelligentPx.Services
{
    internal class AutoSuggestService : IAutoSuggestService
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings["Cognitive_BingAutosuggest_ApiKey"];
        private static readonly string ApiRootUrl = "https://api.cognitive.microsoft.com/bing/v5.0/suggestions";

        private readonly HttpClient _httpClient = CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ApiRootUrl)
            };
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);
            return client;
        }

        public async Task<string[]> Suggest(string query)
        {
            var response = await _httpClient.GetAsync($"?q={query}");
            response.EnsureSuccessStatusCode();

            dynamic result = await response.Content.ReadAsAsync<JObject>();
            var a = ((IEnumerable<dynamic>)result.suggestionGroups[0].searchSuggestions).Select(s => (string)s.query).ToArray();
            return a;
        }
    }
}