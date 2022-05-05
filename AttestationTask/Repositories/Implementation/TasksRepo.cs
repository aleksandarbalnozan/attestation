using AttestationTask.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttestationTask.Repositories
{
    public class TasksRepo : ITasksRepo
    {
        private readonly EntityContext _dbContext;

        public TasksRepo(EntityContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<TaskEntity> GetTaskAsync(Guid id)
        {
            return await _dbContext.TaskEntities.FindAsync(id);
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksAsync()
        {
            return await _dbContext.TaskEntities.ToListAsync();
        }

        public async Task CreateTaskAsync(TaskEntity taskEntity)
        {
            _dbContext.TaskEntities.Add(taskEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskEntity taskEntity)
        {
            _dbContext.Entry(taskEntity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var task = await _dbContext.TaskEntities.FindAsync(id);

            _dbContext.TaskEntities.Remove(task);

            await _dbContext.SaveChangesAsync();
        }
    }
}
