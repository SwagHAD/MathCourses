using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DataMath.Interfaces;
using DataMath.RealizationsInterfaces;
using DataMath.InterfacesRepository;

namespace DataMath.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MathContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMathContext, MathContext>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ITeacherRepisitory, TeacherRepository>();
            return services;
        }
    }
}
