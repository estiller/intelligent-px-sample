using System.Globalization;
using System.Linq;
using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.Services;
using Xamarin.Forms;

namespace IntelligentPx.PageModels
{
    public class TextAnalyticsPageModel : FreshBasePageModel
    {
        private readonly ITextAnalyticsService _textAnalyticsService;

        public TextAnalyticsPageModel(ITextAnalyticsService textAnalyticsService)
        {
            _textAnalyticsService = textAnalyticsService;
        }

        public override async void Init(object initData)
        {
            var photoComments = (PhotoComments)initData;
            var documents = photoComments.Comments.Select(c => new Document
            {
                Id = c.Id.ToString(CultureInfo.InvariantCulture),
                Text = c.Body
            });
            var analyzedText = await _textAnalyticsService.AnalyzeTextAsync(documents);
            DisplayCells = analyzedText.Select(t => new DisplayCell
                {
                    Text = t.Text,
                    Detail = $"Language: {t.Language}, Sentiment: {t.Sentiment}",
                    ImageSource = ImageSource.FromResource($"IntelligentPx.Resources.Sentiment.{GetResourceName(t.Sentiment)}.jpg")
                })
                .ToArray();
        }


        public DisplayCell[] DisplayCells { get; private set; }

        private string GetResourceName(double sentiment)
        {
            if (sentiment > 0.66)
                return "Green";
            return sentiment < 0.33 ? "Red" : "Yellow";
        }

        public class DisplayCell
        {
            public string Text { get; set; }
            public string Detail { get; set; }
            public ImageSource ImageSource { get; set; }
        }
    }
}