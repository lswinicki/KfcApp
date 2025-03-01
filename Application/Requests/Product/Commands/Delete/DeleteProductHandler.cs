using Application.Common.Interfaces.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Requests.Product.Commands.Delete;

public class DeleteProductHandler(IProductRepository productRepository, DeleteProductCommandValidator validator) : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);
        
        var product = await productRepository.GetByIdAsync(request.ProductId);
        await productRepository.DeleteAsync(product, cancellationToken);
        
        return true;
    }
    
    private async Task Validate(DeleteProductCommand request, CancellationToken cancellationToken)
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
        {
            throw new ProblemException("Invalid product ID.", "This product does not exist.");
        }
    }
}