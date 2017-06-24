using System.Threading.Tasks;
using FreshMvvm;

namespace IntelligentPx.PageModels.CognitiveActions
{
    public interface ICognitiveAction
    {
        Task Navigate(FreshBasePageModel current);
    }

    internal class CognitiveAction<TPageModel> : ICognitiveAction where TPageModel : FreshBasePageModel
    {
        public CognitiveAction(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public Task Navigate(FreshBasePageModel current)
        {
            return current.CoreMethods.PushPageModel<TPageModel>();
        }
    }
}