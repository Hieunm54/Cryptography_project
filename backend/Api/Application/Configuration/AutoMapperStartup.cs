using APITemplate;
using AutoMapper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperStartup {
        public static IServiceCollection AddAutoMapperModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            return services;
        }
    }
}
