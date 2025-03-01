namespace Application.Requests.Product.Queries.GetById;

public class GetProductByIdDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CategoryName { get; set; }
}