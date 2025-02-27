using Grpc.Net.Client.Web;
using MathWeb.Grpc;

namespace MathWeb.Extensions
{
    public static class GrpcClientExtensions
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<GroupService.GroupServiceClient>(options =>
            {
                options.Address = new Uri(configuration["Grpc:GroupServiceUrl"]);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new GrpcWebHandler(new HttpClientHandler());
            });

            services.AddGrpcClient<StudentService.StudentServiceClient>(options =>
            {
                options.Address = new Uri(configuration["Grpc:StudentServiceUrl"]);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new GrpcWebHandler(new HttpClientHandler());
            });

            return services;
        }
    }
}
