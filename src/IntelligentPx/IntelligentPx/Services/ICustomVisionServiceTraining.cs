using System.Collections.Generic;
using System.Threading.Tasks;
using IntelligentPx.Models.CustomVision;

namespace IntelligentPx.Services
{
    public interface ICustomVisionServiceTraining
    {
        Task<Project[]> GetProjects();
        Task<Project> CreateProject(string name, string description, string domainId);
        Task<ProjectDomain[]> GetDomains();
        Task<TagCollection> GetTags(string projectId);
        Task<Tag> CreateTag(string projectId, string name, string description);
        Task CreateImages(string projectId, string tagId, IEnumerable<string> urls);
        Task TrainProject(string projectId);
        Task<Iteration[]> GetIterations(string projectId);
    }
}