using System.Linq;
using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.Models.CustomVision;
using IntelligentPx.Services;
using Xamarin.Forms;

namespace IntelligentPx.PageModels
{
    public class CustomVisionPredictionPageModel : FreshBasePageModel
    {
        private readonly ICustomVisionServiceTraining _customVisionServiceTraining;
        private readonly ICustomVisionServicePrediction _customVisionServicePrediction;
        private Project _selectedProject;

        public CustomVisionPredictionPageModel(
            ICustomVisionServiceTraining customVisionServiceTraining,
            ICustomVisionServicePrediction customVisionServicePrediction)
        {
            _customVisionServiceTraining = customVisionServiceTraining;
            _customVisionServicePrediction = customVisionServicePrediction;
        }

        public override async void Init(object initData)
        {
            Photo = (Photo) initData;
            Projects = await _customVisionServiceTraining.GetProjects();
        }

        public Photo Photo { get; private set; }

        public Project[] Projects { get; private set; }

        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                if (_selectedProject != null)
                    GetCompletedIterations.Execute(null);
            }
        }

        public Iteration[] Iterations { get; private set; }

        public Iteration SelectedIteration { get; set; }

        public Prediction Prediction { get; private set; }
        
        public Command Analyze => new Command(async () =>
        {
            Prediction = await _customVisionServicePrediction.Predict(SelectedProject.Id, SelectedIteration.Id, Photo.Images.FullImage.HttpsUrl);
        }, () => SelectedProject?.Id != null && SelectedIteration?.Id != null);

        public Command GetCompletedIterations => new Command(async () =>
        {
            Iterations = (await _customVisionServiceTraining.GetIterations(SelectedProject.Id)).Where(i => i.Status == "Completed").ToArray();
        }, () => SelectedProject?.Id != null);
    }
}