using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using PCLAppConfig;

namespace IntelligentPx.Services
{
    internal class EmotionService : IEmotionService
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings["Cognitive_Emotion_ApiKey"];
        private static readonly string ApiRootUrl = ConfigurationManager.AppSettings["Cognitive_Emotion_ApiRootUrl"];

        public Task<Emotion[]> RecognizeAsync(string imageUrl)
        {
            var emotionClient = new EmotionServiceClient(ApiKey, ApiRootUrl);
            return emotionClient.RecognizeAsync(imageUrl);
        }
    }
}