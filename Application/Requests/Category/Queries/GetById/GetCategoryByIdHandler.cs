using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Requests.Category.Queries.GetById;

public class GetCategoryByIdHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdDto?>
{
    public async Task<GetCategoryByIdDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id);

        if (category == null)
        {
            throw new ProblemException("Invalid category ID.", "This category does not exist.");    
        }
        return mapper.Map<GetCategoryByIdDto>(category);
    }
}