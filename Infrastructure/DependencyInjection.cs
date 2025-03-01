using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Repositories.RabbitMq;
using Domain.Entities;
using Infrastructure.Options;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Infrastructure.Repositories.RabbitMq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using ZiggyCreatures.Caching.Fusion;
using Microsoft.AspNetCore.Http;

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
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        return services;
    }

    public static void InjectRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        var redisOptions = configuration.GetSection(RedisOptions.ConfigName).Get<RedisOptions>();

        if (redisOptions is { RedisEnabled: true, Host: not null })
        {
            var redisConnConfig = new ConfigurationOptions
            {
                EndPoints =
                {
                    {
                        redisOptions.Host,
                        redisOptions.Port
                    }
                },
                //ClientName = "my-redis",
                SyncTimeout = redisOptions.RedisSyncTimeout,
                Ssl = redisOptions.RedisSsl,
                AbortOnConnectFail = redisOptions.RedisAbortOnConnectFail,
                DefaultDatabase = redisOptions.RedisDatabase
                //Password = redisOptions.Password
            };
            
            services.AddStackExchangeRedisCache(config => { config.ConfigurationOptions = redisConnConfig; });
        }

        services.AddFusionCacheNewtonsoftJsonSerializer();

        services.AddFusionCache();
    }

    public static void InjectRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IRabbitMqRepository, RabbitMqRepository>();
    }
}