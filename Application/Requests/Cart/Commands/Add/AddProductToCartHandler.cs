using System.Security.Claims;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Helpers;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Requests.Cart.Commands.Add;

public class AddProductToCartHandler(
    ICacheService cacheService,
    IProductRepository productRepository,
    IUserRepository userRepository,
    IHttpContextAccessor contextAccessor, AddProductToCartValidator validator) : IRequestHandler<AddProductToCartCommand, bool>
{
    public async Task<bool> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);
        var user = await UserInfoHelper.GetUserFromEmail(contextAccessor, userRepository);

        Domain.Models.Cart cart = new Domain.Models.Cart();

        if (user != null)
        {
            var isCartExist = await cacheService.GetValueAsync<Domain.Models.Cart>("Cart - " + user.Id.ToString());
            if (isCartExist != null)
                cart = isCartExist;
        }

        var product = await productRepository.GetByIdAsync(request.ProductId);
        
        CartItem cartItem = cart.Products.FirstOrDefault(x => x.Product.Name == product.Name);
        if (cartItem != null)
        {
            cartItem.Quantity += request.Quantity;
        }
        else
        {
            cart.Products.Add(new CartItem()
            {
                Product = product,
                Quantity = request.Quantity
            });
        }
        
        cart.TotalPrice = cart.TotalPrice + product.Price * request.Quantity;

        await cacheService.SetAsync("Cart - " + user.Id.ToString(), cart, 0, 2, 0, cancellationToken);
        return true;
    }
    
    private async Task Validate(AddProductToCartCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
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