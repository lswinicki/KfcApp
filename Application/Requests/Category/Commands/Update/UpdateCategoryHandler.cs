using Application.Common.Interfaces.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Requests.Category.Commands.Update;

public class UpdateCategoryHandler(ICategoryRepository categoryRepository, UpdateCategoryCommandValidator validator) : IRequestHandler<UpdateCategoryCommand, bool>
{
    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        Domain.Entities.Category category = await categoryRepository.GetByIdAsync(request.CategoryId);

        Domain.Entities.Category categoryUpdated = new Domain.Entities.Category()
        {
            Id = category.Id,
            Name = request.Name
        };

        await categoryRepository.UpdateAsync(categoryUpdated, cancellationToken);
        return true;
    }
    
    private async Task Validate(UpdateCategoryCommand request, CancellationToken cancellationToken)
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
        var categoryByName = await categoryRepository.GetCategoryByNameAsync(request.Name);
        if (category == null)
            throw new ProblemException("Invalid category ID.", "This category does not exist.");
        if (categoryByName != null)
        {
            throw new ProblemException("Invalid category name.", "This category already exists.");
        }
        
    }
}