using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Swag.Web.Auth
{
    public static class AuthServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрирует авторизацию. HTTP-клиенты регистрируются отдельно: AddWebApi&lt;AuthorizedHttpHandler&gt;().
        /// Синглтоны: обработчики HttpClient живут в отдельном scope фабрики,
        /// а блокировка refresh и уведомления о входе должны быть общими на всё приложение.
        /// </summary>
        public static IServiceCollection AddWebAuth(this IServiceCollection services)
        {
            services.AddSingleton<TokenStorage>();
            services.AddSingleton<JwtAuthenticationStateProvider>();
            services.AddSingleton<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthenticationStateProvider>());
            services.AddSingleton<AuthSession>();
            services.AddTransient<AuthorizedHttpHandler>();
            services.AddAuthorizationCore();
            services.AddCascadingAuthenticationState();
            return services;
        }
    }
}
