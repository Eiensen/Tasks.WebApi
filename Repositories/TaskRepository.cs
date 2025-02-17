using Microsoft.EntityFrameworkCore;
using Tasks.WebApi.Context;
using Tasks.WebApi.Entities;

namespace Tasks.WebApi.Repositories;

public class TaskRepository(
    TaskContext context
    ) : IRepository<TaskEntity>
{
    public async Task<TaskEntity> CreateAsync(TaskEntity task)
    {
        context.Add( task );
        await context.SaveChangesAsync();
        return task;
    }

    public async Task<IEnumerable<TaskEntity>> GetAllAsync() => await context.Tasks.ToListAsync();
}
