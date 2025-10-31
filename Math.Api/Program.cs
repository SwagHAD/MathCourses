using Application.Extensions;
using DotNetEnv;
using Infrastructure.Extensions;

public sealed class Program
{
    private static async Task Main(string[] args)
    {
        Env.Load();
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddInfrastructure(GetConnectionStringFromEnv());
        builder.Services.AddApplication();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.MapControllers();
        await app.RunAsync();
    }

    private static string GetConnectionStringFromEnv()
    {
        var host = Env.GetString("db_host");
        var port = Env.GetString("db_port");
        var dbName = Env.GetString("db_name");
        var user = Env.GetString("db_user");
        var password = Env.GetString("db_password");

        return $"Host={host};Port={port};Database={dbName};Username={user};Password={password}";
    }
}