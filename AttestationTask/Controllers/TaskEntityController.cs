using AttestationTask.Dtos;
using AttestationTask.Entitites;
using AttestationTask.Extensions;
using AttestationTask.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttestationTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskEntityController : ControllerBase
    {
        private readonly ITasksRepo repository;

        public TaskEntityController(ITasksRepo repository)
        {
            this.repository = repository;
        }

        // GET /tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksAsync()
        {
            var taskEntity = (await repository.GetTasksAsync()).Select(task => task.AsDto());

            if (taskEntity is null)
            {
                return NotFound();
            }
            return Ok(taskEntity);
        }

        // GET /tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskAsync(Guid id)
        {
            var taskEntity = await repository.GetTaskAsync(id);

            if (taskEntity is null)
            {
                return NotFound();
            }

            return taskEntity.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            TaskEntity taskEntity = new()
            {
                TaskId = Guid.NewGuid(),
                TaskName = createTaskDto.TaskName,
                TaskDescription = createTaskDto.TaskDescription,
                TaskCreationDate = DateTimeOffset.Now,
                ProjectId = createTaskDto.ProjectId
            };

            await repository.CreateTaskAsync(taskEntity);

            return CreatedAtAction(nameof(GetTaskAsync), new { id = taskEntity.TaskId }, taskEntity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTaskAsync(Guid id, UpdateTaskDto updateTask)
        {
            var existingTask = await repository.GetTaskAsync(id);

            if (existingTask is null)
            {
                return NotFound();
            }

            TaskEntity taskEntity = existingTask with
            {
                TaskName = updateTask.TaskName,
                TaskDescription = updateTask.TaskDescription,
                ProjectId = updateTask.ProjectId
            };

            await repository.UpdateTaskAsync(taskEntity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            var task = await repository.GetTaskAsync(id);

            if (task is null)
            {
                return NotFound();
            }

            await repository.DeleteTaskAsync(id);

            return NoContent();
        }
    }
}
