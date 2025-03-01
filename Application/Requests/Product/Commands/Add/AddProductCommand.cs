using MediatR;

namespace Application.Requests.Product.Commands.Add;

public class AddProductCommand : IRequest<bool>
{
    public string? Name { get; set; }
    public int CategoryId { get; set; }
    public decimal? Price { get; set; }
    public string? Description{ get; set; }
}