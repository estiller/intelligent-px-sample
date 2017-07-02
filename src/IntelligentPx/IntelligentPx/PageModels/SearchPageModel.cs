using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.PageModels.ProjectTraining;
using IntelligentPx.Services;
using PCLStorage;
using Xamarin.Forms;

namespace IntelligentPx.PageModels
{
    public class SearchPageModel : FreshBasePageModel
    {
        private readonly IPhotoService _photoService;
        private readonly IAutoSuggestService _autoSuggestService;
        private readonly ISpeechRecognition _speechRecognition;
        private readonly IUserDialogs _userDialogs;

        private IDisposable _suggestionObserver;

        public SearchPageModel(IPhotoService photoService, IAutoSuggestService autoSuggestService, ISpeechRecognition speechRecognition, IUserDialogs userDialogs)
        {
            _photoService = photoService;
            _autoSuggestService = autoSuggestService;
            _speechRecognition = speechRecognition;
            _userDialogs = userDialogs;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            _suggestionObserver = Observable
                .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(h => PropertyChanged += h, h => PropertyChanged -= h)
                .Where(ep => ep.EventArgs.PropertyName == nameof(SearchText))
                .Select(ep => SearchText)
                .Throttle(TimeSpan.FromSeconds(0.5))
                .DistinctUntilChanged()
                .SelectMany((string searchText, CancellationToken ct) => _autoSuggestService.Suggest(searchText))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(suggestions =>
                {
                    Suggestions = suggestions;
                });
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            _suggestionObserver.Dispose();
            _suggestionObserver = null;
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

        public string[] Suggestions { get; set; }

        public Command Search => new Command(async () => await SearchAsync());

        public Command PhotoSelected => new Command(async photo =>
        {
            await CoreMethods.PushPageModel<PhotoDetailsPageModel>(photo);
            SelectedPhoto = null;
        });

        public Command TrainProject => new Command(async () => await CoreMethods.PushPageModel<TrainProjectPageModel>(PhotoCollection));

        public Command SpeechRecognition => new Command(async () => await RecognizeSpeech());

        private async Task SearchAsync()
        {
            if (SearchText != null)
            {
                PhotoCollection = await _photoService.SearchPhotosAsync(SearchText);
            }
        }
        private async Task RecognizeSpeech()
        {
            if (Device.RuntimePlatform != Device.Windows)
            {
                await _userDialogs.AlertAsync("Only on Windows...");
                return;
            }

            var recorder = FreshIOC.Container.Resolve<IWavRecorder>();
            await recorder.StartRecordAsync();
            await _userDialogs.AlertAsync("Speak!", okText: "Stop");

            var filePath = await recorder.EndRecordAsync();
            SearchText = await _speechRecognition.RecognizeSpeechAsync(filePath);

            await (await FileSystem.Current.GetFileFromPathAsync(filePath)).DeleteAsync();
        }
    }
}