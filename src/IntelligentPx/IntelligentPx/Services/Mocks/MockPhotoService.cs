﻿using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IntelligentPx.Models;
using Newtonsoft.Json;

namespace IntelligentPx.Services.Mocks
{
    internal class MockPhotoService : IPhotoService
    {
        public Task<PhotoCollection> GetPhotosAsync(string featureName)
        {
            string response = ReadResponse();
            var result = JsonConvert.DeserializeObject<PhotoCollection>(response);
            return Task.FromResult(result);
        }

        public Task<PhotoCollection> SearchPhotosAsync(string searchTerm)
        {
            return GetPhotosAsync(null);
        }

        public Task<PhotoComments> GetComments(int photoId)
        {
            return Task.FromResult(new PhotoComments());
        }

        private static string ReadResponse()
        {
            using (var stream = typeof(MockPhotoService).Assembly().GetManifestResourceStream("IntelligentPx.Services.Mocks.CachedResponse.json"))
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
