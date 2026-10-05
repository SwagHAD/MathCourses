using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedKernel.AspNetCore.MiddleWares;
using SharedKernel.AspNetCore.Options;
using SharedKernel.AspNetCore.Redis;
using SharedKernel.AspNetCore.UserServices;
using SharedKernel.Enums;
using SharedKernel.Interfaces;
using StackExchange.Redis;
using System.Text;

namespace SharedKernel.AspNetCore
{
    public static class SharedKernelServices
    {
        /// <summary>
        /// Регистрирует общее для всех микросервисов: JWT-аутентификацию, кэш доступов в Redis,
        /// провайдер текущего пользователя и middleware обработки ошибок и проверки доступов.
        /// </summary>
        public static IServiceCollection AddSharedKernel(this IServiceCollection services,
            IConfiguration configuration, ServiceType serviceType)
        {
            services.Configure<ServiceOptions>(options => options.ServiceType = serviceType);
            AddRedis(services, configuration);
            AddUserServices(services);
            AddAuth(services, configuration);
            AddMiddleWares(services);
            return services;
        }

        /// <summary>Подключает обработку ошибок и проверку доступов. Вызывать после UseAuthorization.</summary>
        public static IApplicationBuilder UseSharedKernel(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleWare>();
            app.UseMiddleware<PermissionCheckerMiddleWare>();
            return app;
        }

        private static void AddRedis(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis") ?? 
                    throw new ArgumentNullException("Redis connection string is not configured")));
            services.AddSingleton<IPermissionCache, RedisPermissionCache>();
        }

        private static void AddUserServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserProvider, UserProvider>();
        }

        private static void AddAuth(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection(JwtOptions.SectionName))
                .ValidateDataAnnotations()
                .Validate(options => !string.IsNullOrEmpty(options.SecretKey), "Jwt section is missing or SecretKey is not set")
                .Validate(options => !string.IsNullOrEmpty(options.Issuer), "Jwt section is missing or Issuer is not set")
                .Validate(options => !string.IsNullOrEmpty(options.Audience), "Jwt section is missing or Audience is not set")
                .ValidateOnStart();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<IOptions<JwtOptions>>((bearerOptions, jwtOptions) =>
                {
                    var jwt = jwtOptions.Value;
                    bearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey))
                    };
                });
            services.AddAuthorization();
        }

        private static void AddMiddleWares(IServiceCollection services)
        {
            services.AddScoped<ErrorHandlingMiddleWare>();
            services.AddScoped<PermissionCheckerMiddleWare>();
        }
    }
}
