using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Interfaces;
using SharedKernel.Persistence.Interceptors;

namespace SharedKernel.Persistence
{
    public static class SharedPersistenceServices
    {
        /// <summary>Регистрирует контекст сервиса как <see cref="ISwagDbContext"/> поверх Postgres.</summary>
        public static IServiceCollection AddSwagDbContext<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : BaseDbContext
        {
            services.AddDbContext<ISwagDbContext, TContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres") ?? throw new ArgumentNullException("Postgres connection string is not configured"))
                    .AddInterceptors(new BaseEntityInterceptor()));
            return services;
        }
    }
}
