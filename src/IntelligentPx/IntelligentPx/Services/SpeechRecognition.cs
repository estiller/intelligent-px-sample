using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCLAppConfig;
using PCLStorage;

namespace IntelligentPx.Services
{
    internal class SpeechRecognition : ISpeechRecognition
    {
        private static readonly Guid AppId = Guid.Parse("c5852112-ceaf-4bc2-8d41-7bff1bcd026d");  // Some GUID
        private static readonly Guid DeviceId = Guid.Parse("efed1ca9-a6df-4376-9086-81a93693e2e4");  // Some GUID

        private static readonly string ApiKey = ConfigurationManager.AppSettings["Cognitive_BingSpeech_ApiKey"];
        private static readonly string TokenRootUrl = "https://api.cognitive.microsoft.com/sts/v1.0/";
        private static readonly string ApiRootUrl = "https://speech.platform.bing.com/recognize";

        private readonly HttpClient _httpTokenClient = CreateHttpTokenClient();
        private readonly HttpClient _httpClient = CreateHttpClient();

        private static HttpClient CreateHttpTokenClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(TokenRootUrl)
            };
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);
            return client;
        }

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ApiRootUrl)
            };
            return client;
        }

        public async Task<string> RecognizeSpeechAsync(string filePath)
        {
            var file = await FileSystem.Current.GetFileFromPathAsync(filePath);
            using (var fileStream = await file.OpenAsync(FileAccess.Read))
            {

                string bearerToken = await FetchTokenAsync();
                var response = await SendRequestAsync(fileStream, $"?scenarios=ulm&locale=en-US&format=json&instanceid={DeviceId}&appid={AppId}&requestid={Guid.NewGuid()}&device.os=Xamarin&version=3.0", bearerToken);
                dynamic speechResults = JsonConvert.DeserializeObject<JObject>(response);
                if (speechResults.header.status != "success" || speechResults.header.properties.HIGHCONF != 1)
                    return string.Empty;
                return speechResults.results[0].name;
            }
        }

        private async Task<string> FetchTokenAsync()
        {
            var result = await _httpTokenClient.PostAsync("issueToken", null);
            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync();
        }


        private async Task<string> SendRequestAsync(Stream fileStream, string url, string bearerToken)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StreamContent(fileStream) {Headers =
                {
                    ContentType = new MediaTypeHeaderValue("audio/wav")
                    {
                        Parameters =
                        {
                            new NameValueHeaderValue("codec", "\"audio/pcm\""),
                            new NameValueHeaderValue("samplerate", "16000")
                        }
                    }
                }},
                Headers = {Authorization = new AuthenticationHeaderValue("Bearer", bearerToken)}
            };

            var response = await _httpClient.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}