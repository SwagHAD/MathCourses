using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DataMath.Extensions
{
    public static class DIContainerForDB
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not defined.");
            }

            services.AddDbContext<MathContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IMathContext, MathContext>(); // Используем интерфейс

            return services;
        }
    }
}
