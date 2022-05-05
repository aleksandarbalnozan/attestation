namespace AttestationTask.Dtos
{
    public record UpdateProjectDto
    {
        public string ProjectName { get; init; }
        public string ProjectCode { get; init; }
    }
}
