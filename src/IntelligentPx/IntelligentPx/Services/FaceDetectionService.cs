using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using PCLAppConfig;

namespace IntelligentPx.Services
{
    internal class FaceDetectionService : IFaceDetectionService
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings["Cognitive_Face_ApiKey"];
        private static readonly string ApiRootUrl = ConfigurationManager.AppSettings["Cognitive_Face_ApiRootUrl"];

        private static readonly FaceAttributeType[] FaceAttributeTypes =
        {
            FaceAttributeType.Accessories,
            FaceAttributeType.Age,
            FaceAttributeType.Blur,
            FaceAttributeType.Emotion,
            FaceAttributeType.Exposure,
            FaceAttributeType.FacialHair,
            FaceAttributeType.Gender,
            FaceAttributeType.Glasses,
            FaceAttributeType.Hair,
            FaceAttributeType.HeadPose,
            FaceAttributeType.Makeup,
            FaceAttributeType.Noise,
            FaceAttributeType.Occlusion,
            FaceAttributeType.Smile
        };

        public Task<Face[]> DetectFaces(string imageUrl)
        {
            var faceClient = new FaceServiceClient(ApiKey, ApiRootUrl);
            return faceClient.DetectAsync(imageUrl, true, true, FaceAttributeTypes);
        }
    }
}