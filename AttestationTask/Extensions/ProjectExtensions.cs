using AttestationTask.Dtos;
using AttestationTask.Entitites;

namespace AttestationTask.Extensions
{
    public static class ProjectExtensions
    {
        public static ProjectDto AsDto(this ProjectEntity projectEntity)
        {
            return new ProjectDto
            {
                ProjectId = projectEntity.ProjectId,
                ProjectName = projectEntity.ProjectName,
                ProjectCode = projectEntity.ProjectCode,
                ProjectCreationDate = projectEntity.ProjectCreationDate,
            };
        }
    }
}
