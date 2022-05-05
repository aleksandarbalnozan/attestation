using System;
using System.ComponentModel.DataAnnotations;

namespace AttestationTask.Entitites
{
    public record TaskEntity
    {
        [Key]
        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset TaskCreationDate { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
    }
}
