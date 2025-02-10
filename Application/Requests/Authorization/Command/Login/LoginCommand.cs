using MediatR;

namespace Application.Requests.Authorization.Command.Login;

public class LoginCommand : IRequest<LoginDto?>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
