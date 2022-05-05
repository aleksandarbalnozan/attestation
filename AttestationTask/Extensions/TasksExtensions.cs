using AttestationTask.Dtos;
using AttestationTask.Entitites;

namespace AttestationTask.Extensions
{
    public static class TasksExtensions
    {
        public static TaskDto AsDto(this TaskEntity taskEntity)
        {
            return new TaskDto
            {
                TaskId = taskEntity.TaskId,
                TaskName = taskEntity.TaskName,
                TaskDescription = taskEntity.TaskDescription,
                TaskCreationDate = taskEntity.TaskCreationDate,
                ProjectId = taskEntity.ProjectId,
            };
        }
    }
}
