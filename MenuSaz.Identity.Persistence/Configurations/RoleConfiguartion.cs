using MenuSaz.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuSaz.Identity.Persistence.Configurations
{
    public class RoleConfiguartion : EntityConfiguration<Role>
    {
        public override string TableName => "Role";

        public override string Schema => "";

        public override void ConfigureDerived(EntityTypeBuilder<Role> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
