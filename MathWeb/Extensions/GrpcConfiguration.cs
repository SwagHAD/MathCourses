using Grpc.Net.ClientFactory;

namespace MathWeb.Extensions
{
    public static class GrpcConfiguration
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
        public static Action<GrpcClientFactoryOptions> ConfigureClient(IConfiguration configuration)
        {
            return options =>
            {
                options.Address = new Uri(configuration["Grpc:ServerUrl"]);
            };
        }
    }
}
