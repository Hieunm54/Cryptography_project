using Api.Base.ExceptionFilter;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExceptionHandlerExtension
    {
        public static IApplicationBuilder UseExceptionHanler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionFilterMiddleware>();
            return app;
        }
    }
}
