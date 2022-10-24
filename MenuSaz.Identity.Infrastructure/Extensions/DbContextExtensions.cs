using MenuSaz.Identity.Domain.Models;
using MenuSaz.Identity.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MenuSaz.Identity.Infrastructure.Extensions;
public static class DbContextExtensions
{
    public static void Seed(IApplicationBuilder app)
    {

        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = serviceScope.ServiceProvider;

            using (var context = serviceProvider.GetRequiredService<UserIdentityContext>())
            {
                context.Database.EnsureCreated();

                context.AddRole();
                context.AddUser();

                context.SaveChanges();
                context.Dispose();
            }

        }
    }

    private static void AddRole(this UserIdentityContext context)
    {
        if (!context.Set<Role>().Any())
            context.AddRange(new Role("user", true));
    }
    private static void AddUser(this UserIdentityContext context)
    {
        if (!context.Set<User>().Any())
        {
            var user = new User("test", "test", "test", "test", true);
            context.Add(user);
        }
    }
}
