using AttestationTask.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttestationTask.Repositories
{
    public interface IProjectRepo
    {
        Task<IEnumerable<ProjectEntity>> GetProjectsAsync();
        Task<ProjectEntity> GetProjectAsync(Guid id);
        Task CreateProjectAsync(ProjectEntity project);
        Task UpdateProjectAsync(ProjectEntity project);
        Task DeleteProjectAsync(Guid id);
    }
}