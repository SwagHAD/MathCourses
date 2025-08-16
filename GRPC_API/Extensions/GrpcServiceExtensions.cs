using GRPC_API.Services;

namespace GRPC_API.Extensions
{
    public static class GrpcServiceExtensions
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services)
        {
            services.AddGrpc();
            services.AddSingleton<GreeterService>();

            return services;
        }
    }
}
