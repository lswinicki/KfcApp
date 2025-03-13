using MediatR;

namespace Application.Requests.Cart.Commands.DeleteProduct;

public class DeleteProductFromCartCommand : IRequest<bool>
{
    public int ProductId { get; set; }
}