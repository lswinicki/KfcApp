using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Requests.Product.Commands.Add;

public class AddProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper, AddProductCommandValidator validator) : IRequestHandler<AddProductCommand, bool>
{
    public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);
        
        var newProduct = mapper.Map<Domain.Entities.Product>(request);
        
        await productRepository.AddAsync(newProduct, cancellationToken);

        return true;
    }

    
    private async Task Validate(AddProductCommand request, CancellationToken cancellationToken)
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
            throw new ProblemException("Invalid category ID.", "This category does not exist.");    
    }
}