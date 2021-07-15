using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using APITemplate.Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DatabaseConfiguration {
        public static IServiceCollection AddDatabaseModule(this IServiceCollection services, IConfiguration configuration)
        {
            //var entityFrameworkConfiguration = configuration.GetSection("EntityFramework");

            var connection = configuration.GetConnectionString("ApplicationDatabase");

            services.AddDbContext<ApplicationDatabaseContext>(options =>
                options.UseMySql(connection, ServerVersion.AutoDetect(connection)
            ));

            services.AddScoped<DbContext>(provider => provider.GetService<ApplicationDatabaseContext>());

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var loggingContext = scope.ServiceProvider.GetService<ApplicationDatabaseContext>();

                try
                {
                    loggingContext.Database.Migrate();
                    //await EnsureSeedDataAsync(context);
                }
                catch (Exception e)
                {
                    //logger.LogError($"SeedData Exception: {e.Message} \n {e.StackTrace}");
                }
            }

            return services;
        }

        public static IApplicationBuilder UseApplicationDatabase(this IApplicationBuilder app,
            IServiceProvider serviceProvider, IHostEnvironment environment)
        {
            if (environment.IsDevelopment() || environment.IsProduction())
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDatabaseContext>();
                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();
                }
            }

            return app;
        }
    }
}
