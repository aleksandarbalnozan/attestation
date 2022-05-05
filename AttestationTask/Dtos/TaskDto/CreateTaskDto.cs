using System;
using System.ComponentModel.DataAnnotations;

namespace AttestationTask.Dtos
{
    public record CreateTaskDto
    {
        [Required]
        public string TaskName { get; init; }
        [Required]
        public string TaskDescription { get; init; }
        [Required]
        public Guid ProjectId { get; set; }
    }
}
