using IntelligentPx.Models;

namespace IntelligentPx.Extensions
{
    public static class PhotoExtensions
    {
        private const int LongRibSize = ImageCollection.FullImageSize;

        public static int GetFullImageHeight(this Photo photo)
        {
            return photo.Height >= photo.Width ? LongRibSize : (int)(photo.Height * ((double)LongRibSize / photo.Width));
        }

        public static int GetFullImageWidth(this Photo photo)
        {
            return photo.Width >= photo.Height ? LongRibSize : (int)(photo.Width * ((double)LongRibSize / photo.Height));
        }
    }
}