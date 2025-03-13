using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Requests.Cart.Commands.DeleteCart;

public class DeleteCartHandler(IHttpContextAccessor contextAccessor, IUserRepository userRepository, ICacheService cacheService) : IRequestHandler<DeleteCartCommand, bool>
{
    public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var user = await UserInfoHelper.GetUserFromEmail(contextAccessor, userRepository);
        
        await cacheService.RemoveAsync("Cart - " + user.Id);
        return true;
    }
}