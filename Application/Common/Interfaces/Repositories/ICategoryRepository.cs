using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface ICategoryRepository : IBasicRepository<Category>
{
    Task<Category?> GetCategoryByNameAsync(string name, CancellationToken cancellationToken = default);
}