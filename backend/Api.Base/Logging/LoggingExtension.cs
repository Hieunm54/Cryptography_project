using Api.Base.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LoggingExtension
    {

        public static IServiceCollection AddLoggingAsync(
            this IServiceCollection services,
            [NotNull] Action<CustomLoggingOptions> handlerOptions)
        {
            if (handlerOptions == null) throw new ArgumentNullException(nameof(handlerOptions));
            var option = new CustomLoggingOptions();
            handlerOptions.Invoke(option);
            if (string.IsNullOrWhiteSpace(option.ServiceName))
            {
                throw new ArgumentNullException("Servicename field required not null");
            }

            if (string.IsNullOrWhiteSpace(option.ConnectionString))
            {
                throw new ArgumentNullException("ConnectionString field required not null");
            }

            services.AddSingleton(option);
            services.AddDbContext<LoggingDbContext>(opt =>
                opt.UseMySql(
                    option.ConnectionString,
                    ServerVersion.AutoDetect(option.ConnectionString)
                )
            );

            // add Migration
            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var loggingContext = scope.ServiceProvider.GetService<LoggingDbContext>();

                try
                {
                    loggingContext.Database.Migrate();
                    //await EnsureSeedDataAsync(context);
                }
                catch (Exception)
                {
                    //logger.LogError($"SeedData Exception: {e.Message} \n {e.StackTrace}");
                }
            }
            // add Migration

            return services;
        }

        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggingMiddleware>();
            return app;
        }
    }
}
