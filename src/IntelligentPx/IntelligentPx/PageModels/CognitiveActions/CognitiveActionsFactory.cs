namespace IntelligentPx.PageModels.CognitiveActions
{
    public interface ICognitiveActionsFactory
    {
        ICognitiveAction[] GetActions();
    }

    internal class CognitiveActionsFactory : ICognitiveActionsFactory
    {
        public ICognitiveAction[] GetActions()
        {
            return new ICognitiveAction[]
            {
            };
        }
    }
}