using AttestationTask.Dtos;
using AttestationTask.Entitites;
using AttestationTask.Extensions;
using AttestationTask.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttestationTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectEntityController : Controller
    {
        private readonly IProjectRepo _repository;
        private readonly ILogger<ProjectEntityController> _logger;
        public ProjectEntityController(IProjectRepo _repository, ILogger<ProjectEntityController> _logger)
        {
            this._repository = _repository;
            this._logger = _logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectDto>> GetProjectsAsync()
        {
            var projects = (await _repository.GetProjectsAsync()).Select(project => project.AsDto());

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {projects.Count()}");

            return projects;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectAsync(Guid id)
        {
            var project = await _repository.GetProjectAsync(id);

            if (project is null)
            {
                return NotFound();
            }

            return project.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProjectAsync(CreateProjectDto createProjectDto)
        {
            ProjectEntity projectEntity = new()
            {
                ProjectId = Guid.NewGuid(),
                ProjectName = createProjectDto.ProjectName,
                ProjectCode = createProjectDto.ProjectCode,
                ProjectCreationDate = DateTimeOffset.Now
            };

            await _repository.CreateProjectAsync(projectEntity);

            return CreatedAtAction(nameof(GetProjectAsync), new { id = projectEntity.ProjectId }, projectEntity.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProjectAsync(Guid id, UpdateProjectDto projectDto)
        {
            var existingProject = await _repository.GetProjectAsync(id);

            if (existingProject is null)
            {
                return NotFound();
            }

            ProjectEntity projectEntity = existingProject with
            {
                ProjectName = projectDto.ProjectName,
                ProjectCode = projectDto.ProjectCode,
            };

            await _repository.UpdateProjectAsync(projectEntity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectAsync(Guid id)
        {
            var existingProject = await _repository.GetProjectAsync(id);

            if (existingProject is null)
            {
                return NotFound();
            }

            await _repository.DeleteProjectAsync(id);

            return NoContent();
        }
    }
}
