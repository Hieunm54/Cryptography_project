//using Api.Base.Log;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.EntityFrameworkCore;

//namespace Microsoft.Extensions.DependencyInjection
//{
//    public static class ActionAuditLoggingExtension
//    {

//        public static IServiceCollection AddActionAuditLogging(this IServiceCollection services, string connectionString)
//        {
//            services.AddDbContext<LoggingDbContext>(opt =>
//                opt.UseMySql(
//                    connectionString,
//                    ServerVersion.AutoDetect(connectionString)
//                )
//            );
//            return services;
//        }

//        public static IApplicationBuilder UseActionAuditLogging(this IApplicationBuilder app)
//        {
//            app.UseMiddleware<ActionAuditLoggingMiddleware>();
//            return app;
//        }
//    }
//}
