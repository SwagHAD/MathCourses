using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataMath
{
    public class MathContextFactory : IDesignTimeDbContextFactory<MathContext>
    {
        public MathContext CreateDbContext(string[] args)
        {
            // Загрузка переменных окружения из .env файла, расположенного в другом проекте
            DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { "../MathgRPCServer/.env" }));

            // Чтение переменных окружения
            var DB_NAME = Environment.GetEnvironmentVariable("DB_NAME");
            var DB_PORT = Environment.GetEnvironmentVariable("DB_PORT");
            var DB_HOST = Environment.GetEnvironmentVariable("DB_HOST");
            var DB_USER = Environment.GetEnvironmentVariable("DB_USER");
            var DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");

            if (string.IsNullOrEmpty(DB_NAME) || string.IsNullOrEmpty(DB_HOST))
            {
                throw new InvalidOperationException("Не удалось прочитать переменные окружения из .env файла.");
            }

            var connectionString =
                $"Host={DB_HOST};Port={DB_PORT};Database={DB_NAME};Username={DB_USER};Password={DB_PASSWORD}";

            var optionsBuilder = new DbContextOptionsBuilder<MathContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new MathContext(optionsBuilder.Options);
        }
    }
}
