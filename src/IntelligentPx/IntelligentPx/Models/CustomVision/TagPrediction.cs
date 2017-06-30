namespace IntelligentPx.Models.CustomVision
{
    public class TagPrediction
    {
        public string TagId { get; set; }
        public string Tag { get; set; }
        public double Probability { get; set; }
    }
}