using Application.Extensions;
using DotNetEnv;
using Infrastructure.Extensions;
using Math.Api;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        Env.Load();
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddInfrastructure(DatabaseInitializer.GetConnectionStringFromEnv());
        builder.Services.AddApplication();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        await DatabaseInitializer.MigrateAsync(app.Services);
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.MapControllers();
        await app.RunAsync();
    }
}