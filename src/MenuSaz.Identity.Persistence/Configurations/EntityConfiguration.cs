using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nitro.Fund.Backend.Domain.Common;

namespace MenuSaz.Identity.Persistence.Configurations
{
    public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity<long>
    {
        public abstract string TableName { get; }

        public abstract string Schema { get; }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (string.IsNullOrEmpty(Schema))
                builder.ToTable(TableName);
            else
                builder.ToTable(TableName, Schema);

            builder.HasKey(x => x.Id);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(e => e.Version)
                .IsRowVersion();

            ConfigureDerived(builder);
        }

        public abstract void ConfigureDerived(EntityTypeBuilder<TEntity> builder);

    }
}
