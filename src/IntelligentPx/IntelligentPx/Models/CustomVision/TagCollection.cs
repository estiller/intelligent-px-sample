namespace IntelligentPx.Models.CustomVision
{
    public class TagCollection
    {
        public Tag[] Tags { get; set; }
        public int TotalTaggedImages { get; set; }
        public int TotalUntaggedImages { get; set; }
    }
}