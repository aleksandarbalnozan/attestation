using AttestationTask.Entitites;
using System;
using System.Collections.Generic;

namespace AttestationTask.Dtos
{
    public class TaskDto
    {
        public Guid TaskId { get; init; }
        public string TaskName { get; init; }
        public string TaskDescription { get; init; }
        public DateTimeOffset TaskCreationDate { get; set; }
        public Guid ProjectId { get; set; }
    }
}
