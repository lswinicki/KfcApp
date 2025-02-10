using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("KfcBaseConnection")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        services.AddScoped(typeof(IBasicRepository<>), typeof(BasicRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
