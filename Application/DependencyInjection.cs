using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}