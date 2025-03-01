using MediatR;

namespace Application.Requests.Product.Commands.Delete;

public class DeleteProductCommand : IRequest<bool>
{
    public int ProductId { get; set; }
}