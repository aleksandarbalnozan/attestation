using AttestationTask.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttestationTask.Repositories
{
    public class ProjectRepo : IProjectRepo
    {
        private readonly List<ProjectEntity> projectEntities = new()
        {
            new ProjectEntity
            {
                ProjectId = Guid.NewGuid(),
                ProjectName = "Project Name Example 1",
                ProjectCode = "Project Code Example 1",
                ProjectCreationDate = DateTime.Now
            },
            new ProjectEntity
            {
                ProjectId = Guid.NewGuid(),
                ProjectName = "Project Name Example 2",
                ProjectCode = "Project Code Example 2",
                ProjectCreationDate = DateTime.Now
            },
            new ProjectEntity
            {
                ProjectId = Guid.NewGuid(),
                ProjectName = "Project Name Example 3",
                ProjectCode = "Project Code Example 3",
                ProjectCreationDate = DateTime.Now
            }
        };

        private readonly EntityContext _dbContext;

        public ProjectRepo(EntityContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<IEnumerable<ProjectEntity>> GetProjectsAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<ProjectEntity> GetProjectAsync(Guid id)
        {
            return await _dbContext.Projects.FindAsync(id);
        }

        public async Task CreateProjectAsync(ProjectEntity project)
        {
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(ProjectEntity project)
        {
            _dbContext.Entry(project).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            var project = await _dbContext.Projects.FindAsync(id);

            _dbContext.Projects.Remove(project);

            await _dbContext.SaveChangesAsync();
        }
    }
}
