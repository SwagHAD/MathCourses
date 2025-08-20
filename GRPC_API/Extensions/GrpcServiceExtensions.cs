using GRPC_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GRPC_API.Extensions
{
    public static class GrpcServiceExtensions
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();
            services.AddSingleton<GreeterService>();

            // Добавляем аутентификацию (пример с JWT)
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["Jwt:Authority"];
                    options.Audience = configuration["Jwt:Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            // Добавляем авторизацию
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAuthenticatedUser", policy =>
                    policy.RequireAuthenticatedUser());

                // Можно добавить дополнительные политики
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));
            });

            return services;
        }

        public static IApplicationBuilder UseGrpcServices(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
            });
            return app;
        }
        public static IApplicationBuilder UseAuthServices(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
