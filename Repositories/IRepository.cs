namespace Tasks.WebApi.Repositories;

public interface IRepository<T>
{
    Task<T> CreateAsync(T task);

    Task<IEnumerable<T>> GetAllAsync();
}