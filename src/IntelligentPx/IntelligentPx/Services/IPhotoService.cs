using System.Threading.Tasks;
using IntelligentPx.Models;

namespace IntelligentPx.Services
{
    public interface IPhotoService
    {
        Task<PhotoCollection> GetPhotosAsync(string featureName);

        Task<PhotoCollection> SearchPhotosAsync(string searchTerm);

        Task<PhotoComments> GetComments(int photoId);
    }

    public static class PhotoStreamFeatures
    {
        public const string Upcoming = "upcoming";
        public const string Popular = "popular";
    }
}