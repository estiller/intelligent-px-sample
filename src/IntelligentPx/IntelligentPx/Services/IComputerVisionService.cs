using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision.Contract;

namespace IntelligentPx.Services
{
    public interface IComputerVisionService
    {
        Task<AnalysisResult> Analyze(string imageUrl);
    }
}