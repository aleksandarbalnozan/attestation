using Microsoft.EntityFrameworkCore;

namespace AttestationTask.Entitites
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions options) : base(options) { }

        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<TaskEntity> TaskEntities { get; set; }
    }
}
