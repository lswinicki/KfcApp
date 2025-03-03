using Application.Requests.Product.Commands.Add;
using Application.Requests.Product.Commands.Delete;
using Application.Requests.Product.Commands.Update;
using Application.Requests.Product.Queries.GetAll;
using Application.Requests.Product.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;      
    }

    [HttpGet]
    [Route("GetProducts")]
    [Authorize]
    public async Task<ActionResult<List<GetAllProductsDto>>> GetProducts()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }
    
    [HttpGet]
    [Route("GetProductById/{id}")]
    public async Task<ActionResult<GetProductByIdDto?>> GetProductById(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery{Id = id});
        return Ok(product);
    }

    [HttpPost]
    [Route("AddProduct")]
    public async Task<IActionResult> AddProduct(AddProductCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete]
    [Route("DeleteProduct")]
    public async Task<IActionResult> DeleteProduct(DeleteProductCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpPut]
    [Route("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}