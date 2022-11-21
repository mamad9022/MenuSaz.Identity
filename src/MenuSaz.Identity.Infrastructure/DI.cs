using MenuSaz.Identity.Application.Repositories;
using MenuSaz.Identity.Application.Services;
using MenuSaz.Identity.Application.UnitOfWork;
using MenuSaz.Identity.Infrastructure.Repository;
using MenuSaz.Identity.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MenuSaz.Identity.Infrastructure;
public static class DI
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}
