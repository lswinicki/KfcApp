using Application.Requests.Category.Commands.Add;
using Application.Requests.Category.Commands.Delete;
using Application.Requests.Category.Commands.Update;
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
    
    [HttpPost]
    [Route("AddCategory")]
    public async Task<IActionResult> AddCategory(AddCategoryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete]
    [Route("DeleteCategory")]
    public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpPut]
    [Route("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}