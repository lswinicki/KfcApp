using Application.Common.Interfaces.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Requests.Category.Commands.Delete;

public class DeleteCategoryHandler(ICategoryRepository categoryRepository, DeleteCategoryCommandValidator validator, IProductRepository productRepository) : IRequestHandler<DeleteCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);
        var category = await categoryRepository.GetByIdAsync(request.CategoryId);
        
        await categoryRepository.DeleteAsync(category, cancellationToken);
        return true;
    }
    
    private async Task Validate(DeleteCategoryCommand request, CancellationToken cancellationToken)
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
        if (category == null)
        {
            throw new ProblemException("Invalid category ID.", "This category does not exist.");
        }
        var isCategoryUsing = await productRepository.GetByCategoryIdAsync(request.CategoryId);
        if (isCategoryUsing.Any())
        {
            throw new ProblemException("Invalid category.", "This category is used by some product.");
        }
    }
}