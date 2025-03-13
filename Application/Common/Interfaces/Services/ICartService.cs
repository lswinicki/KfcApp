using Domain.Models;

namespace Application.Common.Interfaces.Services;

public interface ICartService
{
    Task<Cart> GetCartAsync(int userId, CancellationToken cancellationToken = default);
    Task SetCartAsync(Cart cart, CancellationToken cancellationToken = default);
}