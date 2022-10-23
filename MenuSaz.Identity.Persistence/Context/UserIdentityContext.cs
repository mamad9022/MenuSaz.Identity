using MenuSaz.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
