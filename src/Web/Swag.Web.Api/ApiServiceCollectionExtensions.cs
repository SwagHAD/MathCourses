using Microsoft.Extensions.DependencyInjection;
using Swag.Web.Api.Services.Interfaces;
using Swag.Web.Api.Services.Internal;

namespace Swag.Web.Api
{
    public static class ApiServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрирует typed-клиенты к сервисам: наружу видны только интерфейсы из Services.Interfaces.
        /// <typeparamref name="TAuthHandler"/> подставляет токен в запросы — передаётся снаружи,
        /// чтобы этот проект не зависел от проекта авторизации.
        /// </summary>
        public static IServiceCollection AddWebApi<TAuthHandler>(this IServiceCollection services, ApiOptions options)
            where TAuthHandler : DelegatingHandler
        {
            var authUrl = new Uri(options.AuthBaseUrl);
            var coreUrl = new Uri(options.CoreBaseUrl);

            // Без обработчика токенов: refresh через этот обработчик зациклился бы сам на себя
            services.AddHttpClient<IAuthApi, AuthApi>(client => client.BaseAddress = authUrl);

            services.AddAuthorizedClient<IUserService, UserService, TAuthHandler>(authUrl);
            services.AddAuthorizedClient<IRoleService, RoleService, TAuthHandler>(authUrl);
            services.AddAuthorizedClient<IPermissionService, PermissionService, TAuthHandler>(authUrl);

            services.AddAuthorizedClient<IStudentService, StudentService, TAuthHandler>(coreUrl);
            services.AddAuthorizedClient<ITeacherService, TeacherService, TAuthHandler>(coreUrl);
            services.AddAuthorizedClient<IGroupService, GroupService, TAuthHandler>(coreUrl);
            services.AddAuthorizedClient<ICourseService, CourseService, TAuthHandler>(coreUrl);
            return services;
        }

        private static void AddAuthorizedClient<TClient, TImplementation, TAuthHandler>(this IServiceCollection services,
            Uri baseAddress)
            where TClient : class
            where TImplementation : class, TClient
            where TAuthHandler : DelegatingHandler
        {
            services.AddHttpClient<TClient, TImplementation>(client => client.BaseAddress = baseAddress)
                .AddHttpMessageHandler<TAuthHandler>();
        }
    }
}
