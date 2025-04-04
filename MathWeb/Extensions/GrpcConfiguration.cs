using Grpc.Net.ClientFactory;
using MathgRPCServer.Grpc;
using Microsoft.Extensions.DependencyInjection;

namespace MathWeb.Extensions
{
    public static class GrpcConfiguration
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<GroupService.GroupServiceClient>(ConfigureClient(configuration));
            services.AddGrpcClient<StudentService.StudentServiceClient>(ConfigureClient(configuration));
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
