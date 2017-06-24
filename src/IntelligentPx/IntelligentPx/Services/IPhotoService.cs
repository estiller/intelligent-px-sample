using System.Threading.Tasks;
using IntelligentPx.Models;

namespace IntelligentPx.Services
{
    public interface IPhotoService
    {
        Task<PhotoCollection> GetPhotosAsync();

        Task<PhotoCollection> SearchPhotosAsync(string searchTerm);
    }
}