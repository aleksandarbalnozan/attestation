using System;
using System.ComponentModel.DataAnnotations;

namespace AttestationTask.Dtos
{
    public record CreateProjectDto
    {
        [Required]
        public string ProjectName { get; init; }
        [Required]
        public string ProjectCode { get; init; }
    }
}
