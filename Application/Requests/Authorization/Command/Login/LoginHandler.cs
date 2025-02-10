using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Application.Requests.Authorization.Command.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginDto?>
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;

    public LoginHandler(IConfiguration configuration, IUserRepository userRepository, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _userManager = userManager;
    }
    public async Task<LoginDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if(user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user?.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            
            var token = GetToken(authClaims);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var loginDto = new LoginDto()
            {
                Email = request.Email,
                UserName = user.UserName,
                Token = tokenString,
                Roles = userRoles.ToList(),
                Id = user.Id.ToString()
            };
            return loginDto;
        }

        return null;
    }
    
    private JwtSecurityToken GetToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}
