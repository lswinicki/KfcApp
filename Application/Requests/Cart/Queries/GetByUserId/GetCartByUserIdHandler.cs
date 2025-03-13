using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Requests.Cart.Queries.GetByUserId;

public class GetCartByUserIdHandler(
    ICacheService cacheService,
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository)
    : IRequestHandler<GetCartByUserIdQuery, Domain.Models.Cart?>
{
    public async Task<Domain.Models.Cart?> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await UserInfoHelper.GetUserFromEmail(httpContextAccessor, userRepository);

        Domain.Models.Cart? cart;
        if (user == null)
        {
            cart = await cacheService.GetValueAsync<Domain.Models.Cart>(new Guid().ToString(), cancellationToken);
        }
        else
        {
            cart = await cacheService.GetValueAsync<Domain.Models.Cart>("Cart - " + user.Id.ToString(),
                cancellationToken);
        }

        if (cart == null)
        {
            return null;
        }

        return cart;
    }
}