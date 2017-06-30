using System;

namespace IntelligentPx.Models.CustomVision
{
    public class Iteration
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime TrainedAt { get; set; }
        public bool IsDefault { get; set; }
    }
}