using System.Collections.ObjectModel;
using System.Linq;
using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.Models.CustomVision;
using IntelligentPx.Services;
using Xamarin.Forms;

namespace IntelligentPx.PageModels.ProjectTraining
{
    public class TrainProjectPageModel : FreshBasePageModel
    {
        private readonly ICustomVisionServiceTraining _customVisionService;
        private Project _selectedProject;

        public TrainProjectPageModel(ICustomVisionServiceTraining customVisionService)
        {
            _customVisionService = customVisionService;
        }

        public override async void Init(object initData)
        {
            PhotoCollection = (PhotoCollection) initData;
            Projects = new ObservableCollection<Project>(await _customVisionService.GetProjects());
            Tags = new ObservableCollection<Tag>();
        }

        public override void ReverseInit(object returnedData)
        {
            switch (returnedData)
            {
                case Project newProject:
                    Projects.Add(newProject);
                    SelectedProject = newProject;
                    break;
                case Tag newTag:
                    Tags.Add(newTag);
                    SelectedTag = newTag;
                    break;
            }
        }

        public PhotoCollection PhotoCollection { get; private set; }

        public ObservableCollection<Project> Projects { get; set; }

        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                Tags.Clear();
                if (RefreshTags.CanExecute(null))
                    RefreshTags.Execute(null);
            }
        }

        public Command NewProject => new Command(async () => await CoreMethods.PushPageModel<NewTrainingProjectPageModel>());

        public ObservableCollection<Tag> Tags { get; set; }

        public Tag SelectedTag { get; set; }

        public Command NewTag => new Command(async () => await CoreMethods.PushPageModel<NewTagPageModel>(SelectedProject.Id), () => SelectedProject?.Id != null);

        public Command RefreshTags => new Command(async () =>
        {
            var tagCollection = await _customVisionService.GetTags(SelectedProject.Id);
            foreach (var tag in tagCollection.Tags)
            {
                Tags.Add(tag);
            }
        }, () => SelectedProject?.Id != null);

        public Command Upload => new Command(
            async () => await _customVisionService.CreateImages(SelectedProject.Id, SelectedTag.Id, PhotoCollection.Photos.Select(p => p.Images.FullImage.HttpsUrl)), 
            () => SelectedProject?.Id != null && SelectedTag?.Id != null);

        public Command Train => new Command(async () =>
        {
            await _customVisionService.TrainProject(SelectedProject.Id);
            await CoreMethods.PopPageModel();
        }, () => SelectedProject?.Id != null && SelectedTag?.Id != null);
    }
}