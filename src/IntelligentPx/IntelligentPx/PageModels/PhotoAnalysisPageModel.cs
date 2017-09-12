using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.Services;
using Microsoft.ProjectOxford.Vision.Contract;
using Color = Xamarin.Forms.Color;

namespace IntelligentPx.PageModels
{
    public class PhotoAnalysisPageModel : FreshBasePageModel
    {
        private readonly IComputerVisionService _computerVisionService;
        private AnalysisResult _analysisResult;

        public PhotoAnalysisPageModel(IComputerVisionService computerVisionService)
        {
            _computerVisionService = computerVisionService;
        }

        public override void Init(object initData)
        {
            Photo = (Photo)initData;
            Analyze();
        }

        public Photo Photo { get; private set; }

        public AnalysisResult AnalysisResult
        {
            get => _analysisResult;
            private set
            {
                _analysisResult = value;

                BackgroundColor = Color.FromHex(AnalysisResult.Color.AccentColor);
                BackgroundColorSecondary = BackgroundColor.AddLuminosity(0.1);
                TextColor = BackgroundColor.Luminosity > 0.5 ? Color.Black : Color.White;
            }
        }

        public Color BackgroundColor { get; private set; }

        public Color BackgroundColorSecondary { get; private set; }

        public Color TextColor { get; private set; }

        private async void Analyze()
        {
            var url = ToHttp(Photo.Images.FullImage.HttpsUrl);  // ToHttp: Temoporary workaround for Analysis Service with specific TLS version/implementation
            AnalysisResult = await _computerVisionService.Analyze(url);
        }

        private static string ToHttp(string httpsUrl)
        {
            return httpsUrl.Replace("https", "http");
        }
    }
}