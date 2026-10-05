using Microsoft.EntityFrameworkCore;
using SharedKernel.Entities;

namespace SharedKernel.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Общая настройка колонок IBaseEntity для всех сущностей модели.
        /// Вызывать в OnModelCreating после ApplyConfigurationsFromAssembly.
        /// </summary>
        public static ModelBuilder ConfigureBaseEntities(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
                    continue;

                // Значение по умолчанию нужно для строк, которые уже были в таблице до миграции
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(IBaseEntity.CreatedAt))
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }
            return modelBuilder;
        }
    }
}
