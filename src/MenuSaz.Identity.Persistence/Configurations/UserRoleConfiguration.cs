using MenuSaz.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuSaz.Identity.Persistence.Configurations
{
    public class UserRoleConfiguration : EntityConfiguration<UserRole>
    {
        public override string TableName => "UserRole";
        public override string Schema => "";

        public override void ConfigureDerived(EntityTypeBuilder<UserRole> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey(e => new { e.UserId, e.RoleId });

            builder.HasOne(c => c.User)
                .WithMany(x => x.UserRole)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(c => c.Role)
          .WithMany(x => x.UserRole)
          .HasForeignKey(e => e.RoleId);
        }
    }
}
