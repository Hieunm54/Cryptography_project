using Api.Base.ExceptionFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using System.Web.Helpers;

namespace Api.Base.Logging
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate Next;
        private readonly ILogger<LoggingMiddleware> Logger;
        private readonly CustomLoggingOptions options;

        public LoggingMiddleware(
            RequestDelegate next,
            ILogger<LoggingMiddleware> logger,
            CustomLoggingOptions options)
        {
            Next = next;
            Logger = logger;
            this.options = options;
        }

        public async Task Invoke(HttpContext context, LoggingDbContext dbContext)
        {
            string userName = null;
            //string method = null;
            string query = null;
            string controllerName = null;
            string actionName = null;
            string ip = null;
            string userAgent = null;
            string traceId = null;
            int? objectId = null;
            DateTime beginTime = DateTime.UtcNow;

            //get common infomation
            var method = context.Request.Method;
            var requestWithObjectId = method.Equals("POST") || method.Equals("PUT") || method.Equals("DELETE");
            if (requestWithObjectId)
            {
                query = JsonSerializer.Serialize(context.Request?.Query);
                if (context.Request != null && context.Request.Query != null && context.Request.Query.ContainsKey("id"))
                {
                    objectId = int.Parse(context.Request.Query["id"].FirstOrDefault());
                }
            }

            ip = context.Connection?.RemoteIpAddress.ToString();
            traceId = context.TraceIdentifier;
            //var user = JsonSerializer.Serialize(context.User);
            userName = !string.IsNullOrWhiteSpace(context.User?.Identity?.Name) ? context.User?.Identity?.Name : "Admin";

            // Log request header
            if (context.Request != null && context.Request.Headers != null && context.Request.Headers.ContainsKey("User-Agent"))
            {
                userAgent = context.Request.Headers["User-Agent"];
            }
            var requestHeader = JsonSerializer.Serialize(context.Request?.Headers);
            // Log request header

            // Log request body
            context.Request.EnableBuffering();
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            // Log request body
            //get common infomation

            //if request is mapping controller -> must log action
            //else only catch exception
            // Log Controller name and action
            var enpoint = context.GetEndpoint();
            ControllerActionDescriptor controllerActionDescriptor = null;
            if (enpoint != null)
            {
                controllerActionDescriptor = enpoint.Metadata
                                .GetMetadata<ControllerActionDescriptor>();
                if (controllerActionDescriptor != null)
                {
                    controllerName = controllerActionDescriptor.ControllerName;
                    actionName = controllerActionDescriptor.ActionName;
                }
            }
            var mustLogAction = (enpoint != null) && (controllerActionDescriptor != null);
            var originResponseBody = context.Response.Body;
            var isException = false;
            Exception exception = null;
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                isException = true;
                exception = ex;
            }
            finally
            {
                try
                {
                    var actionAudit = new ActionAudit
                    {
                        LogLevel = isException ? (int)Enums.LogLevel.Error : (int)Enums.LogLevel.Info,
                        UserName = userName,
                        Controller = controllerName,
                        Action = actionName,
                        Parameter = query,
                        UserAgent = userAgent,
                        ObjectId = objectId,
                        NewObjectValue = bodyAsText,
                        Ip = ip,
                        BeginAuditTime = beginTime,
                        EndAuditTime = DateTime.UtcNow,
                        TraceId = traceId,
                        LogSource = options.ServiceName
                    };
                    if (isException)
                    {
                        actionAudit.ErrorMessage = $"{exception.Message}: \n {exception.StackTrace}";
                    }
                    if (isException || mustLogAction)
                    {
                        await dbContext.ActionAudit.AddAsync(actionAudit);
                        await dbContext.SaveChangesAsync();
                    }
                }
                catch (Exception exx)
                {
                    Logger.LogError($"UnhandlerException: {exx.Message}\n{exx.StackTrace}");
                }
                if (isException) await HandlerException(context, exception, originResponseBody);
            }
        }


        private async Task HandlerException(
            HttpContext context,
            Exception e,
            Stream originResponse)
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
