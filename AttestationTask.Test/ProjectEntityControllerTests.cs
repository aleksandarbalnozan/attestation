using AttestationTask.Controllers;
using AttestationTask.Dtos;
using AttestationTask.Entitites;
using AttestationTask.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AttestationTask.Test
{
    public class ProjectEntityControllerTests
    {
        private readonly Mock<IProjectRepo> _repoStub = new Mock<IProjectRepo>();
        private readonly Mock<ILogger<ProjectEntityController>> _loggerStub = new Mock<ILogger<ProjectEntityController>>();

        [Fact]
        public async Task GetProjectAsync_WithUnexistingProject_ReturnsNotFound()
        {
            // Arange
            _repoStub.Setup(repo => repo.GetProjectAsync(It.IsAny<Guid>())).ReturnsAsync((ProjectEntity)null);

            var controller = new ProjectEntityController(_repoStub.Object, _loggerStub.Object);

            // Act
            var result = await controller.GetProjectAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        private ProjectEntity CreateRandomProject()
        {
            return new()
            {
                ProjectId = Guid.NewGuid(),
                ProjectName = Guid.NewGuid().ToString(),
                ProjectCode = Guid.NewGuid().ToString(),
                ProjectCreationDate = DateTimeOffset.UtcNow,
            };
        }

        [Fact]
        public async Task GetProjectAsync_WithExistingProject_ReturnsExpectedProject()
        {
            // Arrange
            ProjectEntity expectedProject = CreateRandomProject();

            _repoStub.Setup(repo => repo.GetProjectAsync(It.IsAny<Guid>())).ReturnsAsync(expectedProject);

            var controller = new ProjectEntityController(_repoStub.Object, _loggerStub.Object);

            // Act
            var result = await controller.GetProjectAsync(Guid.NewGuid());

            // Asser
            result.Value.Should().BeEquivalentTo(expectedProject, opt => opt.ComparingByMembers<ProjectEntity>());
        }

        [Fact]
        public async Task GetProjectsAsync_WithExistingProjects_ReturnsAllProject()
        {
            // Arange
            var expectedProjects = new[] { CreateRandomProject(), CreateRandomProject(), CreateRandomProject() };

            _repoStub.Setup(repo => repo.GetProjectsAsync()).ReturnsAsync(expectedProjects);

            var controller = new ProjectEntityController(_repoStub.Object, _loggerStub.Object);

            // Act
            var actualProjects = await controller.GetProjectsAsync();

            // Asset
            actualProjects.Should().BeEquivalentTo(expectedProjects, opt => opt.ComparingByMembers<ProjectEntity>());
        }

        [Fact]
        public async Task CreateProjectAsync_WithProjectToCreate_ReturnsCreatedProject()
        {
            // Arange
            var itemToCreate = new CreateProjectDto()
            {
                ProjectName = Guid.NewGuid().ToString(),
                ProjectCode = Guid.NewGuid().ToString(),
            };

            var controller = new ProjectEntityController(_repoStub.Object, _loggerStub.Object);

            // Act
            var result = await controller.CreateProjectAsync(itemToCreate);

            // Asset
            var createdProject = (result.Result as CreatedAtActionResult).Value as ProjectDto;
            itemToCreate.Should().BeEquivalentTo(createdProject, opt => opt.ComparingByMembers<ProjectDto>().ExcludingMissingMembers());
            createdProject.ProjectId.Should().NotBeEmpty();
            createdProject.ProjectCode.Should().NotBeEmpty();
            createdProject.ProjectName.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateProjectAsync_WithExistingProject_ReturnsNoContent()
        {
            // Arange
            var existingProject = CreateRandomProject();

            _repoStub.Setup(repo => repo.GetProjectAsync(It.IsAny<Guid>())).ReturnsAsync(existingProject);

            var projectId = existingProject.ProjectId;
            var projectToUpdate = new UpdateProjectDto()
            {
                ProjectName = Guid.NewGuid().ToString(),
                ProjectCode = Guid.NewGuid().ToString()
            };

            var controller = new ProjectEntityController(_repoStub.Object, _loggerStub.Object);

            // Act
            var result = await controller.UpdateProjectAsync(projectId, projectToUpdate);

            // Asset
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteProjectAsync_WithExistingProject_ReturnsNoContent()
        {
            // Arange
            var existingProject = CreateRandomProject();

            _repoStub.Setup(repo => repo.GetProjectAsync(It.IsAny<Guid>())).ReturnsAsync(existingProject);

            var projectId = existingProject.ProjectId;

            var controller = new ProjectEntityController(_repoStub.Object, _loggerStub.Object);

            // Act
            var result = await controller.DeleteProjectAsync(projectId);

            // Asset
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
