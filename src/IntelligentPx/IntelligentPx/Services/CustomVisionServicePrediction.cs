using System;
using System.Net.Http;
using System.Threading.Tasks;
using IntelligentPx.Extensions;
using IntelligentPx.Models.CustomVision;
using PCLAppConfig;

namespace IntelligentPx.Services
{
    internal class CustomVisionServicePrediction : ICustomVisionServicePrediction
    {
        private readonly HttpClient _httpClient = CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/")
            };
            client.DefaultRequestHeaders.Add("Prediction-key", ConfigurationManager.AppSettings["Cognitive_CustomVision_PredictionKey"]);
            return client;
        }

        public async Task<Prediction> Predict(string projectId, string iterationId, string url)
        {
            var request = new
            {
                Url = url
            };
            var response = await _httpClient.PostAsJsonAsync($"{projectId}/url?iterationId={iterationId}", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Prediction>();
        }
    }
}