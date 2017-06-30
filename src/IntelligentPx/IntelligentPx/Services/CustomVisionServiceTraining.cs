using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IntelligentPx.Extensions;
using IntelligentPx.Models.CustomVision;
using PCLAppConfig;

namespace IntelligentPx.Services
{
    internal class CustomVisionServiceTraining : ICustomVisionServiceTraining
    {
        private readonly HttpClient _httpClient = CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Training/")
            };
            client.DefaultRequestHeaders.Add("Training-key", ConfigurationManager.AppSettings["Cognitive_CustomVision_TrainingKey"]);
            return client;
        }

        public async Task<Project[]> GetProjects()
        {
            var response = await _httpClient.GetAsync("projects");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Project[]>();
        }

        public async Task<Project> CreateProject(string name, string description, string domainId)
        {
            var response = await _httpClient.PostAsync($"projects?name={name}&description={WebUtility.UrlEncode(description)}&domainId={domainId}", new StringContent(string.Empty));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Project>();
        }

        public async Task<ProjectDomain[]> GetDomains()
        {
            var response = await _httpClient.GetAsync("domains");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ProjectDomain[]>();
        }

        public async Task<TagCollection> GetTags(string projectId)
        {
            var response = await _httpClient.GetAsync($"projects/{projectId}/tags");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TagCollection>();
        }

        public async Task<Tag> CreateTag(string projectId, string name, string description)
        {
            var response = await _httpClient.PostAsync($"projects/{projectId}/tags?name={name}&description={WebUtility.UrlEncode(description)}", new StringContent(string.Empty));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Tag>();
        }

        public async Task CreateImages(string projectId, string tagId, IEnumerable<string> urls)
        {
            var request = new
            {
                TagIds = new[] {tagId},
                Urls = urls
            };
            var response = await _httpClient.PostAsJsonAsync($"projects/{projectId}/images/url", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task TrainProject(string projectId)
        {
            var response = await _httpClient.PostAsync($"projects/{projectId}/train", new StringContent(string.Empty));
            response.EnsureSuccessStatusCode();
        }

        public async Task<Iteration[]> GetIterations(string projectId)
        {
            var response = await _httpClient.GetAsync($"projects/{projectId}/iterations");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Iteration[]>();
        }
    }
}