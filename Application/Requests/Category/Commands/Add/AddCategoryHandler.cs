using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Requests.Category.Commands.Add;

public class AddCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper, AddCategoryCommandValidator validator) : IRequestHandler<AddCategoryCommand, bool>
{
    public async Task<bool> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);
        var newCategory = mapper.Map<Domain.Entities.Category>(request);
        
        await categoryRepository.AddAsync(newCategory, cancellationToken);
        return true;
    }
    
    private async Task Validate(AddCategoryCommand request, CancellationToken cancellationToken)
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
        
        var category = await categoryRepository.GetCategoryByNameAsync(request.Name, cancellationToken);
        
        if (category != null)
            throw new ProblemException("Invalid category name.", "This category already exists.");    
    }
}