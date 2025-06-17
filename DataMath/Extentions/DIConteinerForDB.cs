using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;

namespace DataMath.Extensions
{
    public static class DIContainerForDB
    {
        private static string? DB_NANE = Environment.GetEnvironmentVariable("DB_NAME");
        private static string? DB_PORT = Environment.GetEnvironmentVariable("DB_PORT");
        private static string? DB_HOST = Environment.GetEnvironmentVariable("DB_HOST");
        private static string? DB_USER = Environment.GetEnvironmentVariable("DB_USER");
        private static string? DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {

            var connectionString = $"Host={DB_HOST};Port={DB_PORT};Database={DB_NANE};Username={DB_USER};Password={DB_PASSWORD}";
            services.AddDbContext<MathContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IMathContext, MathContext>();

            return services;
        }
    }
}
