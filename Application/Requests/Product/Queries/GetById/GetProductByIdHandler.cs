using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Requests.Product.Queries.GetById;

public class GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, GetProductByIdDto?>
{
    public async Task<GetProductByIdDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdWithCategoriesAsync(request.Id, cancellationToken);
        if (product == null)
        {
            throw new ProblemException("Invalid product id", "Product does not exist");
        }

        return mapper.Map<GetProductByIdDto>(product);
    }
}