using System.Security.Claims;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Helpers;

public class UserInfoHelper
{
    public static async Task<User?> GetUserFromEmail(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        var currentUser = httpContextAccessor.HttpContext.User;
        string? userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
        return await userRepository.GetByIdAsync(int.Parse(userId));
    }
}