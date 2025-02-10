using System.Net;
using Application.Requests.Authorization.Command.Login;
using Application.Requests.Authorization.Command.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("Login")]
    public async Task <ActionResult<LoginDto>> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        
        return result == null ? Unauthorized() : (LoginDto)result;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<bool>> Register([FromBody] RegisterCommand command)
    {
        return true;
    }
}