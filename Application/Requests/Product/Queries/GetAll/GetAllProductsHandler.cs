using Application.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Requests.Product.Queries.GetAll;

public class GetAllProductsHandler(IProductRepository productRepository, IMapper mapper): IRequestHandler<GetAllProductsQuery, List<GetAllProductsDto>?>
{

    public async Task<List<GetAllProductsDto>?> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllWithCategoriesAsync();
        
        var productResult = mapper.Map<List<GetAllProductsDto>>(products);
        return productResult;
    }
}