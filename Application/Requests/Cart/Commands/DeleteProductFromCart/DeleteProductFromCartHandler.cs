using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Helpers;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Requests.Cart.Commands.DeleteProduct;

public class DeleteProductFromCartHandler(IHttpContextAccessor contextAccessor, IUserRepository userRepository, ICacheService cacheService, IProductRepository productRepository, DeleteProductFromCartValidator fromCartValidator) : IRequestHandler<DeleteProductFromCartCommand, bool>
{
    public async Task<bool> Handle(DeleteProductFromCartCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);
        var user = await UserInfoHelper.GetUserFromEmail(contextAccessor, userRepository);

        var product = await productRepository.GetByIdAsync(request.ProductId);
        var cart = await cacheService.GetValueAsync<Domain.Models.Cart>("Cart - " + user.Id.ToString());

        CartItem cartItem = cart.Products.FirstOrDefault(x => x.Product.Name == product.Name);
        cart.Products.Remove(cartItem);
        await cacheService.SetAsync("Cart - " + user.Id.ToString(), cart, 0, 2, 0, cancellationToken);

        return true;
    }
    
    private async Task Validate(DeleteProductFromCartCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await fromCartValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            string errorMesage = string.Empty;
            foreach (var failure in validationResult.Errors)
            {
                errorMesage = errorMesage + failure.ErrorMessage + "::";
            }
            throw new ProblemValidationException("Validation error", errorMesage);
        }
        var product = await productRepository.GetByIdAsync(request.ProductId);
        
        if (product == null)
            throw new ProblemException("Invalid product.", "This product doesn't exists.");    
    }
}