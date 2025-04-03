using MathWeb.Components;
using MathWeb.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Добавляем поддержку Razor Components (Blazor Server)
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // Добавляем gRPC-клиента
        builder.Services.AddGrpcClients(builder.Configuration);

        var app = builder.Build();

        // Конфигурируем middleware
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
