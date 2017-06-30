using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace IntelligentPx.Services
{
    public interface IEmotionService
    {
        Task<Emotion[]> RecognizeAsync(string imageUrl);
    }
}