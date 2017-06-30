using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IntelligentPx.Extensions;
using IntelligentPx.Models;
using Newtonsoft.Json;
using PCLAppConfig;

namespace IntelligentPx.Services
{
    internal class TextAnalyticsService : ITextAnalyticsService
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings["Cognitive_TextAnalysis_ApiKey"];
        private static readonly string ApiRootUrl = ConfigurationManager.AppSettings["Cognitive_TextAnalysis_ApiRootUrl"];

        private readonly HttpClient _httpClient = CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            var rootUrl = ApiRootUrl.EndsWith("/") ? ApiRootUrl : ApiRootUrl + "/";
            var client = new HttpClient
            {
                BaseAddress = new Uri(rootUrl)
            };
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);
            return client;
        }

        public async Task<AnalyzedText[]> AnalyzeTextAsync(IEnumerable<Document> documents)
        {
            var documentList = documents.ToList();
            var documentDic = documentList.ToDictionary(d => d.Id);

            var languageRequest = new LanguageRequest { Documents = documentList };
            var languageResult = await GetLanguages(languageRequest);
            var languageDocumentDic = languageResult.Documents.ToDictionary(ld => ld.Id);

            var sentimentRequest = new SentimentRequest
            {
                Documents = languageResult.Documents.Select(ld => new SentimentRequestDocument
                {
                    Id = ld.Id,
                    Text = documentDic[ld.Id].Text,
                    Language = ld.DetectedLanguages.First().Iso6391Name
                }).ToList()
            };
            var sentimentResult = await GetSentiment(sentimentRequest);

            return sentimentResult.Documents.Select(sd => new AnalyzedText
                {
                    Id = sd.Id,
                    Text = documentDic[sd.Id].Text,
                    Language = languageDocumentDic[sd.Id].DetectedLanguages.First().Name,
                    Sentiment = sd.Score
                })
                .ToArray();
        }

        private async Task<LanguageResult> GetLanguages(LanguageRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("languages", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<LanguageResult>();
        }

        private async Task<SentimentResult> GetSentiment(SentimentRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("sentiment", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<SentimentResult>();
        }

        #region Helper Classes

        private class LanguageRequest
        {
            [JsonProperty("documents")]
            public List<Document> Documents { get; set; }
        }

        private class SentimentRequest
        {
            [JsonProperty("documents")]
            public List<SentimentRequestDocument> Documents { get; set; }
        }

        private class SentimentRequestDocument
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("text")]
            public string Text { get; set; }
            [JsonProperty("language")]
            public string Language { get; set; }
        }

        private class DetectedLanguage
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("iso6391Name")]
            public string Iso6391Name { get; set; }
            [JsonProperty("score")]
            public double Score { get; set; }
        }

        private class LanguageDocument
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("detectedLanguages")]
            public List<DetectedLanguage> DetectedLanguages { get; set; }
        }

        private class AnalysisError
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("message")]
            public string Message { get; set; }
        }

        private class LanguageResult
        {
            [JsonProperty("documents")]
            public List<LanguageDocument> Documents { get; set; }
            [JsonProperty("errors")]
            public List<AnalysisError> Errors { get; set; }
        }

        private class SentimentDocument
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("score")]
            public double Score { get; set; }
        }


        private class SentimentResult
        {
            [JsonProperty("documents")]
            public List<SentimentDocument> Documents { get; set; }
            [JsonProperty("errors")]
            public List<AnalysisError> Errors { get; set; }
        }

        #endregion
    }
}