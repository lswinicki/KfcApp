namespace Application.Requests.Authorization.Command.Login;

public class LoginDto
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
    public string? UserName { get; set; }
    public List<string>? Roles { get; set; }
}
