using MediatR;

namespace Application.Requests.Cart.Commands.Add;

public class AddProductToCartCommand : IRequest<bool>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}