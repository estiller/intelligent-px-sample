using System.Threading.Tasks;
using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.Services;
using Xamarin.Forms;

namespace IntelligentPx.PageModels
{
    public class PhotosPageModel : FreshBasePageModel
    {
        private readonly IPhotoService _photoService;

        public PhotosPageModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public override void Init(object initData)
        {
            Upcoming.Execute(null);
        }

        public PhotoCollection PhotoCollection { get; set; }

        private Photo _selectedPhoto;
        public Photo SelectedPhoto
        {
            get => _selectedPhoto;
            set
            {
                _selectedPhoto = value;
                if (value != null)
                    PhotoSelected.Execute(value);
            }
        }

        public Command Search => new Command(async () => await CoreMethods.PushPageModel<SearchPageModel>());

        public Command Upcoming => new Command(async () => await RefreshAsync(PhotoStreamFeatures.Upcoming));

        public Command Popular => new Command(async () => await RefreshAsync(PhotoStreamFeatures.Popular));

        public Command PhotoSelected => new Command(async photo =>
        {
            await CoreMethods.PushPageModel<PhotoDetailsPageModel>(photo);
            SelectedPhoto = null;
        });

        private async Task RefreshAsync(string featureName)
        {
            PhotoCollection = await _photoService.GetPhotosAsync(featureName);
        }
    }
}