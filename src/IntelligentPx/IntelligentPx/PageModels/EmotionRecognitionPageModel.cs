using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.Services;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace IntelligentPx.PageModels
{
    public class EmotionRecognitionPageModel : FreshBasePageModel
    {
        private readonly IEmotionService _emotionService;

        public EmotionRecognitionPageModel(IEmotionService emotionService)
        {
            _emotionService = emotionService;
        }

        public override void Init(object initData)
        {
            Photo = (Photo)initData;
            RecognizeEmotions();
        }

        public Photo Photo { get; private set; }

        public Emotion[] Emotions { get; set; }

        private async void RecognizeEmotions()
        {
            Emotions = await _emotionService.RecognizeAsync(Photo.Images.FullImage.HttpsUrl);
        }
    }
}