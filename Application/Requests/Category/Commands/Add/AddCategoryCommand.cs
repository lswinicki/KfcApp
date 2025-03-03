using MediatR;

namespace Application.Requests.Category.Commands.Add;

public class AddCategoryCommand : IRequest<bool>
{
    public string? Name { get; set; }
}