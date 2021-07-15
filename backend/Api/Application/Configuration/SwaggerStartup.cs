using APITemplate.Domain.Services;
using APITemplate.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Scrutor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerStartup
    {
        public static IServiceCollection AddSwaggerModule(this IServiceCollection services)
        {
            services.AddOpenApiDocument();
            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            return app;
        }
    }
}
