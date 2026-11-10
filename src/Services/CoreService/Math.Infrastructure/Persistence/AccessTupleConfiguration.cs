using Domain.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class AccessTupleConfiguration : IEntityTypeConfiguration<AccessTuple>
    {
        public void Configure(EntityTypeBuilder<AccessTuple> builder)
        {
            builder.ToTable("access_tuples");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityAlwaysColumn();
            builder.Ignore(x => x.Key);

            builder.Property(x => x.ObjectType).HasConversion<short>();
            builder.Property(x => x.SubjectType).HasConversion<short>();

            builder.Property(x => x.Relation).HasMaxLength(AccessTuple.RelationMaxLength).IsRequired();
            builder.Property(x => x.SubjectRelation).HasMaxLength(AccessTuple.RelationMaxLength);

            builder.Property(x => x.IsReplica).HasDefaultValue(false);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");

            // Прямой обход (Check: «есть ли у person:42 learner на group:7») + защита от дублей.
            // NULLS NOT DISTINCT: два одинаковых кортежа без SubjectRelation тоже считаются дублем
            builder.HasIndex(x => new { x.ObjectType, x.ObjectId, x.Relation, x.SubjectType, x.SubjectId, x.SubjectRelation })
                .IsUnique()
                .AreNullsDistinct(false)
                .HasDatabaseName("ux_access_tuples_forward");

            // Обратный обход (Filter: «в каких группах person:42») — ObjectId в INCLUDE даёт index-only scan
            builder.HasIndex(x => new { x.SubjectType, x.SubjectId, x.SubjectRelation, x.Relation, x.ObjectType })
                .IncludeProperties(x => x.ObjectId)
                .HasDatabaseName("ix_access_tuples_reverse");
        }
    }
}
