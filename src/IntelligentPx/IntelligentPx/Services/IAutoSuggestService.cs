using System.Threading.Tasks;

namespace IntelligentPx.Services
{
    public interface IAutoSuggestService
    {
        Task<string[]> Suggest(string query);
    }
}