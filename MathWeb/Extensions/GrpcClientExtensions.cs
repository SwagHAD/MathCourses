using Grpc.Net.Client.Web;
using MathgRPCServer.Grpc;

namespace MathWeb.Extensions
{
    public static class GrpcClientExtensions
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<GroupService.GroupServiceClient>(client =>
            {
                client.Address = new Uri("https://localhost:7080");
            });
            services.AddGrpcClient<StudentService.StudentServiceClient>(client =>
            {
                client.Address = new Uri("https://localhost:7080");
            });
            return services;    
        }
    }
}
