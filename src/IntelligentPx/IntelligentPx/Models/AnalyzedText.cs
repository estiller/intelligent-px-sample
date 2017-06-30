namespace IntelligentPx.Models
{
    public class AnalyzedText
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Language { get; set; }
        public double Sentiment { get; set; }
    }
}