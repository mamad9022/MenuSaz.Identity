using MenuSaz.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Nitro.Fund.Backend.Domain.Common;
using System.Reflection;

namespace MenuSaz.Identity.Persistence.Context
{
    public class UserIdentityContext : DbContext
    {
        public UserIdentityContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in ChangeTracker.Entries<Entity<long>>())
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entityEntry.Entity.ModifiedAt = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
