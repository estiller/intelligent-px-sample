using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;

namespace IntelligentPx.Services
{
    public interface IFaceDetectionService
    {
        Task<Face[]> DetectFaces(string imageUrl);
    }
}