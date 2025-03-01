using Application.Common.Interfaces.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Requests.Product.Commands.Update;

public class UpdateProductHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    UpdateProductCommandValidator validator) : IRequestHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        Domain.Entities.Product product = await productRepository.GetByIdAsync(request.ProductId);

        Domain.Entities.Product productUpdated = new Domain.Entities.Product()
        {
            Id = product.Id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            CategoryId = request.CategoryId,
        };

        await productRepository.UpdateAsync(productUpdated);
        return true;
    }


    private async Task Validate(UpdateProductCommand request, CancellationToken cancellationToken)
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

        var category = await categoryRepository.GetByIdAsync(request.CategoryId);
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (category == null)
            throw new ProblemException("Invalid category ID.", "This category does not exist.");
        if (product == null)
            throw new ProblemException("Invalid product.", "This product does not exist.");
    }
}