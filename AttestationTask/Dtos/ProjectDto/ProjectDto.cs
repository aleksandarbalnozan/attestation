using System;

namespace AttestationTask.Dtos
{
    public record ProjectDto
    {
        public Guid ProjectId { get; init; }
        public string ProjectName { get; init; }
        public string ProjectCode { get; init; }
        public DateTimeOffset ProjectCreationDate { get; set; }
    }
}
