using Microsoft.Extensions.DependencyInjection;

namespace Swag.Web.Api
{
    public static class ApiServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрирует HTTP-клиенты к сервисам и <see cref="CoreApi"/>.
        /// <typeparamref name="TAuthHandler"/> подставляет токен в запросы — передаётся снаружи,
        /// чтобы этот проект не зависел от проекта авторизации.
        /// </summary>
        public static IServiceCollection AddWebApi<TAuthHandler>(this IServiceCollection services, ApiOptions options)
            where TAuthHandler : DelegatingHandler
        {
            services.AddHttpClient(HttpClients.AuthAnonymous, client => client.BaseAddress = new Uri(options.AuthBaseUrl));
            services.AddHttpClient(HttpClients.Auth, client => client.BaseAddress = new Uri(options.AuthBaseUrl))
                .AddHttpMessageHandler<TAuthHandler>();
            services.AddHttpClient(HttpClients.Core, client => client.BaseAddress = new Uri(options.CoreBaseUrl))
                .AddHttpMessageHandler<TAuthHandler>();

            services.AddSingleton<CoreApi>();
            return services;
        }
    }
}
