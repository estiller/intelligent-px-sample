using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using IntelligentPx.Extensions;
using IntelligentPx.Models;
using PCLAppConfig;

namespace IntelligentPx.Services
{
    class PhotoService : IPhotoService
    {
        private readonly string _consumerKey = ConfigurationManager.AppSettings["500pxConsumerKey"];

        private readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.500px.com/v1/")
        };

        public async Task<PhotoCollection> GetPhotosAsync(string featureName)
        {
            var response = await _httpClient.GetAsync($"photos?feature={featureName}&exclude=Nude&rpp=50&image_size={ImageCollection.PreviewImageSize},{ImageCollection.FullImageSize}&consumer_key={_consumerKey}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<PhotoCollection>();
        }

        public async Task<PhotoCollection> SearchPhotosAsync(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"photos/search?term={searchTerm}&exclude=Nude&rpp=50&image_size={ImageCollection.PreviewImageSize},{ImageCollection.FullImageSize}&consumer_key={_consumerKey}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<PhotoCollection>();
        }

        public async Task<PhotoComments> GetComments(int photoId)
        {
            var response = await _httpClient.GetAsync($"photos/{photoId.ToString(CultureInfo.InvariantCulture)}/comments?consumer_key={_consumerKey}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<PhotoComments>();
        }
    }
}