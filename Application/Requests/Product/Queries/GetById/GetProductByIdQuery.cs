using MediatR;

namespace Application.Requests.Product.Queries.GetById;

public class GetProductByIdQuery : IRequest<GetProductByIdDto?>
{
    public int Id { get; set; }
}