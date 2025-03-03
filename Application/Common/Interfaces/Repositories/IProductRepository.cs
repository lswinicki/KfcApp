using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IProductRepository : IBasicRepository<Product>
{
    Task<List<Product>?> GetAllWithCategoriesAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetByIdWithCategoriesAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Product>?> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default);
}