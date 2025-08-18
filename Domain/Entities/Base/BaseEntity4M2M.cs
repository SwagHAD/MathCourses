using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Base
{
    /// <summary>
    /// Базовый класс для промежуточных сущностей "многие-ко-многим"
    /// </summary>
    /// <typeparam name="TFirstEntity">Первая сущность</typeparam>
    /// <typeparam name="TSecondEntity">Вторая сущность</typeparam>
    public abstract class BaseManyToManyEntity<TFirstEntity, TSecondEntity>
        where TFirstEntity : BaseEntity
        where TSecondEntity : BaseEntity
    {
        public int FirstEntityId { get; set; }
        public virtual TFirstEntity FirstEntity { get; set; }
        public int SecondEntityId { get; set; }
        public virtual TSecondEntity SecondEntity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Базовый конфигуратор для промежуточных сущностей
    /// </summary>
    public abstract class BaseManyToManyConfiguration<TEntity, TFirstEntity, TSecondEntity>
        : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseManyToManyEntity<TFirstEntity, TSecondEntity>
        where TFirstEntity : BaseEntity
        where TSecondEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => new { x.FirstEntityId, x.SecondEntityId });

            builder.HasOne(x => x.FirstEntity)
                .WithMany()
                .HasForeignKey(x => x.FirstEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.SecondEntity)
                .WithMany()
                .HasForeignKey(x => x.SecondEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            builder.HasIndex(x => new { x.FirstEntityId, x.SecondEntityId })
                .IsUnique();

            ConfigureMore(builder);
        }

        /// <summary>
        /// Дополнительная конфигурация (переопределяется в наследниках)
        /// </summary>
        protected virtual void ConfigureMore(EntityTypeBuilder<TEntity> builder) { }
    }
}