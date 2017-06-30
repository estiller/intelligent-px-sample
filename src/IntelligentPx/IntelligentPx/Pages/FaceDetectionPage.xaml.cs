using System.Collections.Generic;
using System.Reflection;
using FreshMvvm;
using IntelligentPx.Extensions;
using IntelligentPx.PageModels;
using Microsoft.ProjectOxford.Face.Contract;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

namespace IntelligentPx.Pages
{
    public partial class FaceDetectionPage
    {
        private FaceDetectionPageModel _model;

        public FaceDetectionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            _model = (FaceDetectionPageModel) this.GetModel();
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
            if (e.PropertyName == nameof(FaceDetectionPageModel.Faces))
                OnAnalysisResultChanged();
        }

        private void OnAnalysisResultChanged()
        {
            ClearAllFrames();
            if (_model.Faces != null)
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
            var originalHeight = _model.Photo.GetFullImageHeight();
            var originalWidth = _model.Photo.GetFullImageWidth();

            foreach (Face face in _model.Faces)
            {
                var backgroundColor = GetColor(face.FaceAttributes.Gender);

                foreach (var runtimeProperty in typeof(FaceLandmarks).GetRuntimeProperties())
                {
                    var faceBox = new BoxView { BackgroundColor = backgroundColor, Opacity = 0.8 };
                    AddBox(faceBox, (FeatureCoordinate) runtimeProperty.GetValue(face.FaceLandmarks));
                }
            }

            void AddBox(BoxView faceBox, FeatureCoordinate coordinate)
            {
                const int size = 6;

                ImageLayout.Children.Add(faceBox,
                    Constraint.RelativeToView(Image, (rl, v) => (v.X + coordinate.X) * v.Width / originalWidth),
                    Constraint.RelativeToView(Image, (rl, v) => (v.Y + coordinate.Y) * v.Height / originalHeight),
                    Constraint.Constant(size),
                    Constraint.Constant(size));
            }
        }

        private static Color GetColor(string faceGender)
        {
            switch (faceGender)
            {
                case "male":
                    return Color.DeepSkyBlue;
                case "female":
                    return Color.HotPink;
            }
            return Color.Black;
        }
    }
}