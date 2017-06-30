using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.Services;
using Xamarin.Forms;

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
            var photo = (Photo)initData;
            PhotoComments = await _photoService.GetComments(photo.Id);
        }

        public PhotoComments PhotoComments { get; private set; }

        public Command Analyze => new Command(async () => await CoreMethods.PushPageModel<TextAnalyticsPageModel>(PhotoComments));
    }
}