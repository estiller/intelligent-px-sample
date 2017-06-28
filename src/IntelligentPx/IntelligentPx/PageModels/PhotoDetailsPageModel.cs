using FreshMvvm;
using IntelligentPx.Models;

namespace IntelligentPx.PageModels
{
    public class PhotoDetailsPageModel : FreshBasePageModel
    {
        public override void Init(object initData)
        {
            Photo = (Photo) initData;
        }

        public Photo Photo { get; set; }
    }
}