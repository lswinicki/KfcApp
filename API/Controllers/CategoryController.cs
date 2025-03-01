using Application.Requests.Category.Queries.GetAll;
using Application.Requests.Category.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("GetCategories")]
    public async Task<ActionResult<List<GetAllCategoriesDto>>> GetCategories()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());
        return Ok(categories);
    }
    
    [HttpGet]
    [Route("GetCategoryById/{id}")]
    public async Task<ActionResult<GetCategoryByIdDto?>> GetCategoryById(int id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery{Id = id});
        return Ok(category);
    }

}