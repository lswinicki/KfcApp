using MediatR;

namespace Application.Requests.Product.Queries.GetAll;

public class GetAllProductsQuery : IRequest<List<GetAllProductsDto>?>
{

}