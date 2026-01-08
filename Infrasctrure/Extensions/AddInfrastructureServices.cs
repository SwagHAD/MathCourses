using Domain.Entities;
using Domain.Interfaces.Data;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Data;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class AddInfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, string _connectionstring)
        {
            services.AddDbContext(_connectionstring);
            services.AddServices();
            return services;
        }
        private static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IMathDbContext, MathDbContext>(options =>
                options.UseNpgsql(connectionString));
        }
        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<Student>, UnitOfWork<Student>>();
            services.AddScoped<IUnitOfWork<Group>,  UnitOfWork<Group>>();
            services.AddScoped<IUnitOfWork<Lesson>, UnitOfWork<Lesson>>();
            services.AddScoped<IUnitOfWork<Teacher>, UnitOfWork<Teacher>>();
            services.AddScoped<IUnitOfWork<Course>,  UnitOfWork<Course>>();
        }
    }
}
