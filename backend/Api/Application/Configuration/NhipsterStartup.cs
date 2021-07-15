using APITemplate.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NhipsterSettingsConfiguration {
        public static IServiceCollection AddNhipsterModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JHipsterSettings>(configuration.GetSection("jhipster"));
            return services;
        }
    }
}
