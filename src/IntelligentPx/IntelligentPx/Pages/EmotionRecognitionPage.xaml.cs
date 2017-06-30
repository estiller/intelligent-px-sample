using System.Collections.Generic;
using System.Linq;
using FreshMvvm;
using IntelligentPx.Extensions;
using IntelligentPx.PageModels;
using Microsoft.ProjectOxford.Common.Contract;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;
using Emotion = Microsoft.ProjectOxford.Emotion.Contract.Emotion;

namespace IntelligentPx.Pages
{
    public partial class EmotionRecognitionPage
    {
        private EmotionRecognitionPageModel _model;

        public EmotionRecognitionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            _model = (EmotionRecognitionPageModel) this.GetModel();
            _model.PropertyChanged += OnPageModelPropertyChanged;
            OnEmotionsChanged();
        }

        protected override void OnDisappearing()
        {
            _model.PropertyChanged -= OnPageModelPropertyChanged;
            _model = null;
        }

        private void OnPageModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EmotionRecognitionPageModel.Emotions))
                OnEmotionsChanged();
        }

        private void OnEmotionsChanged()
        {
            ClearAllEmotions();
            if (_model.Emotions != null)
            {
                SetNewEmotions();
            }
        }

        private void ClearAllEmotions()
        {
            if (ImageLayout.Children.Count <= 1) return;

            var image = ImageLayout.Children[0];
            ImageLayout.Children.Clear();
            ((IList<View>)ImageLayout.Children).Add(image);
        }

        private void SetNewEmotions()
        {
            var originalHeight = _model.Photo.GetFullImageHeight();
            var originalWidth = _model.Photo.GetFullImageWidth();

            for (var i = 0; i < _model.Emotions.Length; i++)
            {
                Emotion emotion = _model.Emotions[i];
                var strongestEmotion = GetStrongestEmotion(emotion.Scores);
                View faceView;
                if (strongestEmotion != null)
                {
                    faceView = new Image
                    {
                        Source = ImageSource.FromResource($"IntelligentPx.Resources.Emotions.{strongestEmotion}.png"),
                        Aspect = Aspect.AspectFit
                    };
                }
                else
                {
                    faceView = new BoxView { BackgroundColor = Color.Black, Opacity = 0.4 };
                }

                ImageLayout.Children.Add(faceView,
                    Constraint.RelativeToView(Image, (rl, v) => v.X + emotion.FaceRectangle.Left * v.Width / originalWidth),
                    Constraint.RelativeToView(Image, (rl, v) => v.Y + emotion.FaceRectangle.Top * v.Height / originalHeight),
                    Constraint.RelativeToView(Image, (rl, v) => emotion.FaceRectangle.Width * v.Width / originalWidth),
                    Constraint.RelativeToView(Image, (rl, v) => emotion.FaceRectangle.Height * v.Height / originalHeight));

                ImageLayout.Children.Add(new Label {Text = i.ToString(), TextColor = Color.White, FontAttributes = FontAttributes.Bold},
                    Constraint.RelativeToView(faceView, (rl, v) => v.X - 10),
                    Constraint.RelativeToView(faceView, (rl, v) => v.Y + 10));
            }
        }

        private string GetStrongestEmotion(EmotionScores emotionScores)
        {
            var rankedList = emotionScores.ToRankedList().Take(2).ToList();
            if (rankedList[0].Key != "Neutral" && rankedList[0].Value > 0.7)
                return rankedList[0].Key;
            if (rankedList[0].Key == "Neutral" && rankedList[1].Value > 0.2)
                return rankedList[1].Key;
            if (rankedList[0].Value > 0.7)
                return rankedList[0].Key;
            return null;
        }
    }
}