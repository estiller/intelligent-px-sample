using System.Collections.Generic;
using System.Threading.Tasks;
using IntelligentPx.Models;

namespace IntelligentPx.Services
{
    public interface ITextAnalyticsService
    {
        Task<AnalyzedText[]> AnalyzeTextAsync(IEnumerable<Document> documents);
    }
}