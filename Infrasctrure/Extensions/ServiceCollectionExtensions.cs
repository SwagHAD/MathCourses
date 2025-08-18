﻿using DotNetEnv;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static string _connectionstring = $"Host={Env.GetString("DB_Host")};Port={Env.GetString("DB_Port")};" +
                  $"Database={Env.GetString("DB_Name")};Username={Env.GetString("DB_User")};" +
                  $"Password={Env.GetString("DB_Password")}";
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<MathDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString(_connectionstring)));
            return services;
        }
    }
}
