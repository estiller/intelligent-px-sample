using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.Services;

namespace IntelligentPx.PageModels
{
    public class PhotoCommentsPageModel : FreshBasePageModel
    {
        private readonly IPhotoService _photoService;

        public PhotoCommentsPageModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public override async void Init(object initData)
        {
            Photo = (Photo)initData;
            PhotoComments = await _photoService.GetComments(Photo.Id);
        }

        public Photo Photo { get; private set; }

        public PhotoComments PhotoComments { get; private set; }
    }
}