using Application.Interfaces;
using Domain.Entities;
using Infrasctrure.Options;
using Infrasctrure.Postgres;
using Infrasctrure.Redis;
using Infrasctrure.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Infrasctrure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        AddDbContext(services, config);
        AddRedisContext(services, config);
        AddUserServices(services);
        AddAuth(services);
        return services;
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IAuthDbContext, AuthDbContext>(option =>
            option.UseNpgsql(configuration.GetConnectionString("Postgres") ?? throw new ArgumentNullException("Postgres connection string is not configured")));
    }

    private static void AddRedisContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis") ?? throw new ArgumentNullException("Redis connection string is not configured")));
        services.AddSingleton<IPermissionCache, RedisPermissionCache>();
    }

    private static void AddUserServices(IServiceCollection services)
    {
        services.AddScoped<IUserProvider, UserProvider>();
        services.AddSingleton<ITokenProvider, JwtTokenProvider>();
    }

    private static void AddAuth(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IOptions<JwtOptions>>((bearerOptions, jwtOptions) =>
            {
                var jwt = jwtOptions.Value;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey))
                };
            });
        services.AddAuthorization();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
    }
}
