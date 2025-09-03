using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using DotNetEnv;
using Infrastructure.Data;
using Infrastructure.RepositoryServices;
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
            services.AddDbContext<IMathDbContext, MathDbContext>(options =>
                options.UseNpgsql(_connectionstring));
            services.AddRepositories();
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICoreRepository<Student>, StudentService>();
            services.AddScoped<ICoreRepository<Teacher>, TeacherService>();
            services.AddScoped<ICoreRepository<Group>, GroupService>();
            services.AddScoped<ICoreRepository<Lesson>, LessonService>();
            services.AddScoped<ICoreRepository<Course>, CourseService>();
            return services;
        }
    }
}
