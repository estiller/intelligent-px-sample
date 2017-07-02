using System.Threading.Tasks;

namespace IntelligentPx.Services
{
    public interface IWavRecorder
    {
        Task StartRecordAsync();
        Task<string> EndRecordAsync();
    }
}