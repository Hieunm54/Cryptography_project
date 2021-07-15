using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using System.Net;

namespace APITemplate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddControllers();

            services
                .AddDatabaseModule(Configuration)
                .AddAutoMapperModule()
                .AddRepositoryModule()
                .AddServiceModule()
                .AddSwaggerModule();

            var connectionString = Configuration.GetConnectionString("ApplicationDatabase");
            var loggingConnectionString = Configuration.GetConnectionString("LoggingDatabase");

            //services.AddActionAuditLogging(connectionString);
            //services.AddExceptionHandler();

            services.AddLoggingAsync(option =>
            {
                option.ServiceName = "APITemplate";
                option.ConnectionString = loggingConnectionString;
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "api1";

                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    ValidateAudience = false
                    //};
                });

            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            //app.UseExceptionHanler();

            app.UseAppSwagger();

            app.UseRouting();

            app.UseLogging();

            //app.UseActionAuditLogging();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
