using Application.Interfaces;
using Domain.Entities;
using Infrasctrure.Postgres;
using Infrasctrure.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Persistence;

namespace Infrasctrure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSwagDbContext<AuthDbContext>(config);
        AddUserServices(services);
        return services;
    }

    private static void AddUserServices(IServiceCollection services)
    {
        services.AddSingleton<ITokenProvider, JwtTokenProvider>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
    }
}
