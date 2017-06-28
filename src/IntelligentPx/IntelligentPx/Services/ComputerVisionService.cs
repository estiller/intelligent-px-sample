using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using PCLAppConfig;

namespace IntelligentPx.Services
{
    internal class ComputerVisionService : IComputerVisionService
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings["Cognitive_ComputerVision_ApiKey"];
        private static readonly string ApiRootUrl = ConfigurationManager.AppSettings["Cognitive_ComputerVision_ApiRootUrl"];

        private static readonly VisualFeature[] VisualDetails =
        {
            VisualFeature.Categories, VisualFeature.Tags, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Color, VisualFeature.Adult
        };

        private static readonly string[] Details = { "Celebrities", "Landmarks" };

        public Task<AnalysisResult> Analyze(string imageUrl)
        {
            var visionClient = new VisionServiceClient(ApiKey, ApiRootUrl);
            return visionClient.AnalyzeImageAsync(imageUrl, VisualDetails, Details);
        }
    }
}