using Grpc.Net.Client.Web;
using MathWeb.Components;
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
        builder.Services.AddGrpcClient<GroupService.GroupServiceClient>(options =>
        {
            options.Address = new Uri("https://localhost:5001");
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new GrpcWebHandler(new HttpClientHandler());
        });

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
