using Core.Extentions;
using DataMath.Extensions;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.AddConsole();

        builder.Services.AddDataAccess(builder.Configuration);
        builder.Services.AddServices();
        builder.Services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
            options.IgnoreUnknownServices = true;
        });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        app.UseCors("AllowAll");
        app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

        app.Run();

    }
}