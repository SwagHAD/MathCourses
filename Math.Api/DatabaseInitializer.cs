using Domain.Interfaces.Data;
using DotNetEnv;

namespace Math.Api
{
    public static class DatabaseInitializer
    {
        private static string _connectionString = $"Host={Env.GetString("db_host")};Port={Env.GetString("db_port")};Database={Env.GetString("db_name")};Username={Env.GetString("db_user")};Password={Env.GetString("db_password")};";
        public static async Task MigrateAsync(IServiceProvider services)
        {
            await using var scope = services.CreateAsyncScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<IMathDbContext>();
            await dbcontext.MigrateAsync();
        }
        public static string GetConnectionStringFromEnv()
        {
            return _connectionString;
        }
    }
}
