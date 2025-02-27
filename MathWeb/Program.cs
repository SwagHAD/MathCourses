using Grpc.Net.Client.Web;
using MathWeb.Components;
using MathWeb.Extensions;
using MathWeb.Grpc;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ��������� ��������� Razor Components (Blazor Server)
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // ��������� gRPC-�������
        builder.Services.AddGrpcClients(builder.Configuration);

        var app = builder.Build();

        // ������������� middleware
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
