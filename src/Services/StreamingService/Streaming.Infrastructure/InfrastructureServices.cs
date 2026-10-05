using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharedKernel.Persistence;
using StreamingService.Application.Contracts;
using StreamingService.Infrastructure.MediaMtx;
using StreamingService.Infrastructure.Options;
using StreamingService.Infrastructure.Postgres;

namespace StreamingService.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddSwagDbContext<StreamDbContext>(config);
            AddHttpClients(services);
            return services;
        }
        private static void AddHttpClients(IServiceCollection services)
        {
            services.AddHttpClient<IMediaServerClient, MediaMtxClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<MediaMtxOptions>>().Value;
                client.BaseAddress = new Uri(options.ApiUrl);
            });
        }
    }
}
