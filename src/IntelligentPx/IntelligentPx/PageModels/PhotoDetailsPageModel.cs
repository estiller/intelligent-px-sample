using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.PageModels.CognitiveActions;
using Xamarin.Forms;

namespace IntelligentPx.PageModels
{
    public class PhotoDetailsPageModel : FreshBasePageModel
    {
        public PhotoDetailsPageModel(ICognitiveActionsFactory cognitiveActionsFactory)
        {
            CognitiveActions = cognitiveActionsFactory.GetActions();
        }

        public override void Init(object initData)
        {
            Photo = (Photo) initData;
        }

        public Photo Photo { get; set; }

        public ICognitiveAction[] CognitiveActions { get; }

        public ICognitiveAction SelectedAction { get; set; }

        public Command Open => new Command(async () => await SelectedAction.Navigate(this), () => SelectedAction != null);
    }
}