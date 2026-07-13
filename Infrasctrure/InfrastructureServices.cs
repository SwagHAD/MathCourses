using Application.Interfaces;
using Infrastructure.Options;
using Infrastructure.Postgres;
using Infrastructure.Redis;
using Infrastructure.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration config)
        {
            AddDbContext(services, config);
            AddRedis(services, config);
            AddUserServices(services);
            AddAuth(services);
            return services;
        }
        private static void AddDbContext(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ISwagDbContext, SwagDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("Postgres") ?? throw new ArgumentNullException("Postgres connection string is not configured")));
        }
        private static void AddRedis(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(config.GetConnectionString("Redis") ?? throw new ArgumentNullException("Redis connection string is not configured")));
            services.AddSingleton<IPermissionCache, RedisPermissionCache>();
        }
        private static void AddUserServices(IServiceCollection services)
        {
            services.AddScoped<IUserProvider, UserProvider>();
        }
        private static void AddAuth(IServiceCollection services)
        {
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
    }
}
