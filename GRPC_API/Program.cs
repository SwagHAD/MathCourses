using DotNetEnv;
using DotNetEnv.Configuration;
using GRPC_API.Extensions;
using GRPC_API.Services;
using Infrastructure.Extensions;

namespace GRPC_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Env.Load();
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddEnvironmentVariables()
                .AddDotNetEnv();
            builder.Services
                .AddInfrastructure(builder.Configuration)
                .AddGrpcServices(builder.Configuration)
                .AddAuthServices(builder.Configuration);
            var app = builder.Build();
            app.UseAuthServices();
            app.UseGrpcServices();

            await app.RunAsync();
        }
    }
}