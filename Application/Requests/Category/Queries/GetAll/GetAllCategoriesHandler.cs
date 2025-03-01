using Application.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Requests.Category.Queries.GetAll;

public class GetAllCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper): IRequestHandler<GetAllCategoriesQuery, List<GetAllCategoriesDto>>
{
    public async Task<List<GetAllCategoriesDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync();

        var categoriesResult = mapper.Map<List<GetAllCategoriesDto>>(categories);
        return categoriesResult;
    }
}