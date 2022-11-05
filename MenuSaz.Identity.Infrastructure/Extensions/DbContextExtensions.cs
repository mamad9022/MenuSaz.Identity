using MenuSaz.Identity.Domain.Models;
using MenuSaz.Identity.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MenuSaz.Identity.Infrastructure.Extensions;
public static class DbContextExtensions
{
    public static async void Seed(IApplicationBuilder app)
    {

        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = serviceScope.ServiceProvider;

            using (var context = serviceProvider.GetRequiredService<UserIdentityContext>())
            {
                context.Database.EnsureCreated();

                await context.AddRole();
                await context.AddUser();

                await context.SaveChangesAsync();
                context.Dispose();
            }

        }
    }

    private static async Task AddRole(this UserIdentityContext context)
    {
        if (!context.Set<Role>().Any())
            await context.AddRangeAsync(new Role("user", true));
    }
    private static async Task AddUser(this UserIdentityContext context)
    {
        if (!context.Set<User>().Any())
        {
            var user = new User("test", "test", "test", "test", 9036139022, true);
            user.AddRole(new List<Role>
            {
                new Role("user", true)
            }, user);
            await context.AddAsync(user);
        }
    }
}
