using MediatR;

namespace Application.Requests.Product.Commands.Update;

public class UpdateProductCommand : IRequest<bool>
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public string? Description{ get; set; }
}