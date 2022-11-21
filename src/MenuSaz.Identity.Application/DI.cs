using MediatR;
using MenuSaz.Identity.Application.Common;
using MenuSaz.Identity.Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MenuSaz.Identity.Application
{
    public static class DI
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.UseBindingConfig<AppSettings>(configuration, "AppSettings");

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static T UseBindingConfig<T>(this IServiceCollection services, IConfiguration configuration, string configName) where T : class, new()
        {
            var section = configuration.GetSection(configName);
            T config = new();
            section.Bind(config);
            services.Configure<T>(section);

            return config;
;
        }
    }
}
