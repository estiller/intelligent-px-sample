using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.Services;
using Xamarin.Forms;

namespace IntelligentPx.PageModels
{
    public class SearchPageModel : FreshBasePageModel
    {
        private readonly IPhotoService _photoService;

        public SearchPageModel(IPhotoService photoService)
        {
            _photoService = photoService;
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

        public string SearchText { get; set; }

        public Command Search => new Command(SearchAsync);

        public Command PhotoSelected => new Command(async photo => await CoreMethods.PushPageModel<PhotoDetailsPageModel>(photo));

        private async void SearchAsync()
        {
            if (SearchText != null)
            {
                PhotoCollection = await _photoService.SearchPhotosAsync(SearchText);
            }
        }
    }
}