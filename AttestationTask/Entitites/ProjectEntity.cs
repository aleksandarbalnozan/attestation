using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AttestationTask.Entitites
{
    public record ProjectEntity
    {
        [Key]
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public DateTimeOffset ProjectCreationDate { get; set; }
        public ICollection<TaskEntity> Tasks { get; set; }
    }
}
