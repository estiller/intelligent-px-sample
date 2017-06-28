using System.Collections.Generic;
using FreshMvvm;
using IntelligentPx.PageModels;
using Microsoft.ProjectOxford.Vision.Contract;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

namespace IntelligentPx.Pages
{
    public partial class PhotoAnalysisPage
    {
        private PhotoAnalysisPageModel _model;

        public PhotoAnalysisPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            _model = (PhotoAnalysisPageModel) this.GetModel();
            _model.PropertyChanged += OnPageModelPropertyChanged;
            OnAnalysisResultChanged();
        }

        protected override void OnDisappearing()
        {
            _model.PropertyChanged -= OnPageModelPropertyChanged;
            _model = null;
        }

        private void OnPageModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PhotoAnalysisPageModel.AnalysisResult))
                OnAnalysisResultChanged();
        }

        private void OnAnalysisResultChanged()
        {
            ClearAllFrames();
            if (_model.AnalysisResult != null)
            {
                SetNewFrames();
            }
        }

        private void ClearAllFrames()
        {
            if (ImageLayout.Children.Count <= 1) return;

            var image = ImageLayout.Children[0];
            ImageLayout.Children.Clear();
            ((IList<View>)ImageLayout.Children).Add(image);
        }

        private void SetNewFrames()
        {
            foreach (Face face in _model.AnalysisResult.Faces)
            {
                var originalHeight = _model.AnalysisResult.Metadata.Height;
                var originalWidth = _model.AnalysisResult.Metadata.Width;

                var faceBox = new BoxView {BackgroundColor = GetColor(face.Gender), Opacity = 0.4};

                ImageLayout.Children.Add(faceBox,
                    Constraint.RelativeToView(Image, (rl, v) => v.X + face.FaceRectangle.Left * v.Width / originalWidth),
                    Constraint.RelativeToView(Image, (rl, v) => v.Y + face.FaceRectangle.Top * v.Height / originalHeight),
                    Constraint.RelativeToView(Image, (rl, v) => face.FaceRectangle.Width * v.Width / originalWidth),
                    Constraint.RelativeToView(Image, (rl, v) => face.FaceRectangle.Height * v.Height / originalHeight));

                ImageLayout.Children.Add(new Label { Text = face.Age.ToString(), TextColor = Color.White, FontAttributes = FontAttributes.Bold },
                    Constraint.RelativeToView(faceBox, (rl, v) => v.X + 5),
                    Constraint.RelativeToView(faceBox, (rl, v) => v.Y + 5));
            }
        }

        private static Color GetColor(string faceGender)
        {
            switch (faceGender)
            {
                case "Male":
                    return Color.DeepSkyBlue;
                case "Female":
                    return Color.HotPink;
            }
            return Color.Black;
        }
    }
}