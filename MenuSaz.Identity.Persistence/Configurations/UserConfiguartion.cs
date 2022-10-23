using MenuSaz.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuSaz.Identity.Persistence.Configurations
{
    public class UserConfiguartion : EntityConfiguration<User>
    {
        public override string TableName => "User";

        public override string Schema => "";

        public override void ConfigureDerived(EntityTypeBuilder<User> builder)
        {
            builder.Property(t => t.Firstname)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(t => t.Lastname)
               .HasMaxLength(250)
               .IsRequired();

            builder.Property(t => t.Username)
               .HasMaxLength(200)
               .IsRequired();

            builder.Property(t => t.Password)
                .HasMaxLength(200)
                .IsRequired();

            //builder.HasMany(t => t.Roles)
            //.WithMany(x => x.Users)
            //.UsingEntity(j => j.ToTable("UserRole"));
        }
    }
}
