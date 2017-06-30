using System;

namespace IntelligentPx.Models.CustomVision
{
    public class Prediction
    {
        public string Id { get; set; }
        public string Project { get; set; }
        public string Iteration { get; set; }
        public DateTime Created { get; set; }
        public TagPrediction[] Predictions { get; set; }
    }
}