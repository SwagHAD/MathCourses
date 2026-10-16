using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Swag.Web.Api;
using Swag.Web.App;
using Swag.Web.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiOptions = builder.Configuration.GetSection(ApiOptions.SectionName).Get<ApiOptions>()
    ?? throw new InvalidOperationException("В wwwroot/appsettings.json не задана секция Api");

builder.Services.AddWebAuth();
builder.Services.AddWebApi<AuthorizedHttpHandler>(apiOptions);

await builder.Build().RunAsync();
