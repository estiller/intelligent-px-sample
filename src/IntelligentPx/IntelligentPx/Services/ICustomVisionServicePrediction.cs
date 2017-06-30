using System.Threading.Tasks;
using IntelligentPx.Models.CustomVision;

namespace IntelligentPx.Services
{
    public interface ICustomVisionServicePrediction
    {
        Task<Prediction> Predict(string projectId, string iterationId, string url);
    }
}