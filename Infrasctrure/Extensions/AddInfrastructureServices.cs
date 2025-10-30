using Domain.Interfaces.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class AddInfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, string _connectionstring)
        {
            services.AddDbContext<IMathDbContext, MathDbContext>(options =>
                options.UseNpgsql(_connectionstring));
            return services;
        }
    }
}
