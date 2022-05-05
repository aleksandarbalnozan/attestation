using System;

namespace AttestationTask.Dtos
{
    public record UpdateTaskDto
    {
        public string TaskName { get; init; }
        public string TaskDescription { get; init; }
        public Guid ProjectId { get; set; }
    }
}
