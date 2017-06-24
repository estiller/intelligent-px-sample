using System.Collections.Generic;
using System.Linq;

namespace IntelligentPx.Models
{
    public class ImageCollection : List<Image>
    {
        internal const int PreviewImageSize = 2;
        internal const int FullImageSize = 1080;
        
        public Image PreviewImage => this.First(image => image.Size == PreviewImageSize);

        public Image FullImage => this.First(image => image.Size == FullImageSize);
    }
}