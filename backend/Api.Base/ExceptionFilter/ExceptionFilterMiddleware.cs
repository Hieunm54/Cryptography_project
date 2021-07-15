using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Base.ExceptionFilter
{
    public class ExceptionFilterMiddleware
    {
        public RequestDelegate Next { get; set; }
        public ILogger<ExceptionFilterMiddleware> Logger { get; set; }

        public ExceptionFilterMiddleware(RequestDelegate next, ILogger<ExceptionFilterMiddleware> logger)
        {
            Next = next;
            Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var originResponse = context.Response.Body;
            try
            {
                await Next(context);
            }
            catch (Exception e)
            {
                await HandlerException(context, e, originResponse);
            }
        }

        private async Task HandlerException(HttpContext context, Exception e, Stream originResponse)
        {
            ApiErrorResponse result;
            string message;
            if (e is HttpCodeException)
            {
                result = ((HttpCodeException)e).Response;
                message = result.ErrorMesage;
            }
            else
            {
                result = new ApiErrorResponse
                {
                    ErrorCode = 500,
                    ErrorMesage = "Internal Server Error"
                };
                message = $"{e.Message}: \n {e.StackTrace}";
            }

            result.RequestId = context.TraceIdentifier;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.ErrorCode;

            Logger.LogError($"ExceptionFilter: {result.RequestId} - {result.ErrorCode} - {message}");

            // Solution for response stream close when catch exception from other middleware
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;
                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(originResponse);
            }
            context.Response.Body = originResponse;
            // Solution for response stream close when catch exception from other middleware

            await context.Response.WriteAsync(JsonSerializer.Serialize(result));

        }
    }
}
