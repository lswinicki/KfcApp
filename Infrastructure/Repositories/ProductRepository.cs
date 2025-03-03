using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : BasicRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        
    }

    public async Task<List<Product>?> GetAllWithCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(x => x.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdWithCategoriesAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Product>?> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(x => x.Category)
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
    }
}