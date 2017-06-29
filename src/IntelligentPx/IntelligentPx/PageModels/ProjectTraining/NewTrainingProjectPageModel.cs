using System.Threading.Tasks;
using FreshMvvm;
using IntelligentPx.Models.CustomVision;
using IntelligentPx.Services;
using Xamarin.Forms;

namespace IntelligentPx.PageModels.ProjectTraining
{
    public class NewTrainingProjectPageModel : FreshBasePageModel
    {
        private readonly ICustomVisionServiceTraining _customVisionService;

        public NewTrainingProjectPageModel(ICustomVisionServiceTraining customVisionService)
        {
            _customVisionService = customVisionService;
        }

        public string Name { get; set; }

        public ProjectDomain[] Domains { get; private set; }

        public ProjectDomain SelectedDomain { get; set; }

        public string Description { get; set; }

        public override async void Init(object initData)
        {
            base.Init(initData);
            Domains = await _customVisionService.GetDomains();
        }

        public Command Create => new Command(async () => await CreateProject(), () => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description) && SelectedDomain != null);

        private async Task CreateProject()
        {
            var createdProject = await _customVisionService.CreateProject(Name, Description, SelectedDomain.Id);
            await CoreMethods.PopPageModel(createdProject);
        }
    }
}