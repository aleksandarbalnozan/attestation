using AttestationTask.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttestationTask.Repositories
{
    public interface ITasksRepo
    {
        Task<TaskEntity> GetTaskAsync(Guid id);
        Task<IEnumerable<TaskEntity>> GetTasksAsync();
        Task CreateTaskAsync(TaskEntity taskEntity);
        Task UpdateTaskAsync(TaskEntity taskEntity);
        Task DeleteTaskAsync(Guid id);
    }
}