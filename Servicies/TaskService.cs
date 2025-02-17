using Tasks.WebApi.Entities;
using Tasks.WebApi.Repositories;

namespace Tasks.WebApi.Servicies;

public class TaskService(
    IRepository<TaskEntity> repository
    )
{
    public async Task<TaskEntity> CreateAsync(TaskEntity task) => await repository.CreateAsync(task);

    public async Task<IEnumerable<TaskEntity>> GetAllAsync() => await repository.GetAllAsync();
}
