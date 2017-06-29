using System.Threading.Tasks;
using FreshMvvm;
using IntelligentPx.Services;
using Xamarin.Forms;

namespace IntelligentPx.PageModels.ProjectTraining
{
    public class NewTagPageModel : FreshBasePageModel
    {
        private readonly ICustomVisionServiceTraining _customVisionService;
        private string _projectId;

        public NewTagPageModel(ICustomVisionServiceTraining customVisionService)
        {
            _customVisionService = customVisionService;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public override void Init(object initData)
        {
            _projectId = (string) initData;
        }

        public Command Create => new Command(async () => await CreateTag(), () => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description));

        private async Task CreateTag()
        {
            var createdTag = await _customVisionService.CreateTag(_projectId, Name, Description);
            await CoreMethods.PopPageModel(createdTag);
        }
    }
}