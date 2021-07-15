using APITemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace APITemplate
{
    public class SeedData
    {
        public static async Task EnsureSeedDataAsync(IConfiguration configuration, ILogger<Program> logger)
        {
            var connectionString = configuration.GetConnectionString("ApplicationDatabase");
            var loggingConnectionString = configuration.GetConnectionString("LoggingDatabase");
            var services = new ServiceCollection();
            var migrationAssembly = typeof(SeedData).Assembly.FullName;
            services.AddDbContext<ApplicationDatabaseContext>(
                opts => opts.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    sql => sql.MigrationsAssembly(migrationAssembly)));


            services.AddDbContext<LoggingDbContext>(opt => opt.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    sql => sql.MigrationsAssembly(migrationAssembly)
                ));

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDatabaseContext>();
                var loggingContext = scope.ServiceProvider.GetService<LoggingDbContext>();
                
                try
                {
                    await context.Database.MigrateAsync();
                    await loggingContext.Database.MigrateAsync();
                    //await EnsureSeedDataAsync(context);
                }
                catch (Exception e)
                {
                    logger.LogError($"SeedData Exception: {e.Message} \n {e.StackTrace}");
                }
            }
        }

    }
}
