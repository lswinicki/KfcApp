namespace Application.Common.Interfaces.Repositories;

public interface IBasicRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> GetByIdAsync(int id);
}
