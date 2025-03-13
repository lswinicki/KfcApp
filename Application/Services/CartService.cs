using Application.Common.Interfaces.Services;
using Domain.Models;

namespace Application.Services;

public class CartService(ICacheService cacheService) : ICartService
{
    public async Task<Cart> GetCartAsync(int userId, CancellationToken cancellationToken = default)
    {
        var cart = await cacheService.GetValueAsync<Cart>(userId.ToString(), cancellationToken);
        return cart;
    }

    public async Task SetCartAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await cacheService.SetAsync(cart.UserId.ToString() ,cart, 0, 10, 0, cancellationToken);
    }
}