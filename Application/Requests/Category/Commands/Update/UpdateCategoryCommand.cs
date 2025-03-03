using MediatR;

namespace Application.Requests.Category.Commands.Update;

public class UpdateCategoryCommand : IRequest<bool>
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
}