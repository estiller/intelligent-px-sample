using System;

namespace IntelligentPx.Models.CustomVision
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CurrentIterationId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public ProjectSettings Settings { get; set; }
        public string ThumbnailUri { get; set; }
    }
}