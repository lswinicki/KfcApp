using MediatR;

namespace Application.Requests.Category.Queries.GetById;

public class GetCategoryByIdQuery : IRequest<GetCategoryByIdDto?>
{
    public int Id { get; set; }
}