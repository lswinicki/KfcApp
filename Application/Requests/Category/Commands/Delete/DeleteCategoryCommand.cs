using MediatR;

namespace Application.Requests.Category.Commands.Delete;

public class DeleteCategoryCommand : IRequest<bool>
{
    public int CategoryId { get; set; }
}