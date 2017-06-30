using FreshMvvm;
using IntelligentPx.Models;
using Xamarin.Forms;

namespace IntelligentPx.PageModels
{
    public class PhotoDetailsPageModel : FreshBasePageModel
    {
        public override void Init(object initData)
        {
            Photo = (Photo) initData;
        }

        public Photo Photo { get; set; }

        public Command Analyze => new Command(async () => await CoreMethods.PushPageModel<PhotoAnalysisPageModel>(Photo));

        public Command CustomVision => new Command(async () => await CoreMethods.PushPageModel<CustomVisionPredictionPageModel>(Photo));

        public Command Emotion => new Command(async () => await CoreMethods.PushPageModel<EmotionRecognitionPageModel>(Photo));

        public Command FaceDetection => new Command(async () => await CoreMethods.PushPageModel<FaceDetectionPageModel>(Photo));
    }
}