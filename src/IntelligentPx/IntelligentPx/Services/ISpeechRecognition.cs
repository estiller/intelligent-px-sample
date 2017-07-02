using System.Threading.Tasks;

namespace IntelligentPx.Services
{
    public interface ISpeechRecognition
    {
        Task<string> RecognizeSpeechAsync(string filePath);
    }
}