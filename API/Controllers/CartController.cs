using Application.Requests.Cart.Commands.Add;
using Application.Requests.Cart.Commands.DeleteCart;
using Application.Requests.Cart.Commands.DeleteProduct;
using Application.Requests.Cart.Queries.GetByUserId;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : Controller
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("GetCartByUserId")]
    public async Task<ActionResult<Cart?>> GetCartByUserId()
    {
        var cart = await _mediator.Send(new GetCartByUserIdQuery());
        return cart != null ? Ok(cart) : NoContent();
    }
    
    [HttpPost]
    [Route("AddProductToCart")]
    public async Task<IActionResult> AddProductToCart(AddProductToCartCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    [Route("RemoveProductFromCart")]
    public async Task<IActionResult> RemoveProductFromCart(DeleteProductFromCartCommand fromCartCommand)
    {
        await _mediator.Send(fromCartCommand);
        return NoContent();
    }

    [HttpPost]
    [Route("RemoveAllProductsFromCart")]
    public async Task<IActionResult> RemoveAllProductsFromCart()
    {
        DeleteCartCommand command = new DeleteCartCommand();
        await _mediator.Send(command);
        return NoContent();
    }
}