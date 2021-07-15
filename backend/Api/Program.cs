using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace APITemplate
{
    public class Program
    {
        public static async Task /*void*/ Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}", theme: AnsiConsoleTheme.Literate)
                .WriteTo.Logger(l => l.Filter
                        .ByIncludingOnly(x => x.Level == LogEventLevel.Information || x.Level == LogEventLevel.Warning)
                        .WriteTo.File(path: $"logs/info-.log", rollingInterval: RollingInterval.Day))
                .WriteTo.Logger(l => l.Filter
                        .ByIncludingOnly(x => x.Level == LogEventLevel.Error || x.Level == LogEventLevel.Fatal)
                        .WriteTo.File(path: $"logs/error-.log", rollingInterval: RollingInterval.Day))
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();
                var logger = host.Services.GetRequiredService<ILogger<Program>>();
                var config = host.Services.GetRequiredService<IConfiguration>();
                
                await SeedData.EnsureSeedDataAsync(config, logger);
                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal("\n===============Program Terminate unexpected=================\n");
                Log.Fatal($"Exception: {e.Message} \n {e.StackTrace}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}
