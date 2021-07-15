//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Controllers;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Api.Base.Log
//{
//    public class ActionAuditLoggingMiddleware
//    {
//        private readonly RequestDelegate Next;
//        private readonly ILogger<ActionAuditLoggingMiddleware> Logger;

//        public ActionAuditLoggingMiddleware(
//            RequestDelegate next,
//            ILogger<ActionAuditLoggingMiddleware> logger)
//        {
//            Next = next;
//            Logger = logger;
//        }

//        public async Task Invoke(HttpContext context, LoggingDbContext dbContext)
//        {
//            string userName = null;
//            //string method = null;
//            string query = null;
//            string controllerName = null;
//            string actionName = null;
//            string ip = null;
//            string userAgent = null;
//            string traceId = null;
//            int? objectId = null;
//            DateTime beginTime = DateTime.UtcNow;

//            //try
//            //{
//            var method = context.Request.Method;
//            if (!(method.Equals("POST") || method.Equals("PUT") || method.Equals("DELETE")))
//            {
//                await Next(context);
//                return;
//            }

//            query = JsonSerializer.Serialize(context.Request?.Query);
//            if (context.Request != null && context.Request.Query != null && context.Request.Query.ContainsKey("id"))
//            {
//                objectId = int.Parse(context.Request.Query["id"].First());
//            }

//            ip = context.Connection?.RemoteIpAddress.ToString();
//            traceId = context.TraceIdentifier;
//            var user = JsonSerializer.Serialize(context.User);
//            userName = !string.IsNullOrWhiteSpace(context.User?.Identity?.Name) ? context.User?.Identity?.Name : "Admin";

//            //Logger.LogInformation($"method: {method}");
//            //Logger.LogInformation($"query: {query}");
//            //Logger.LogInformation($"Ip: {ip}");
//            //Logger.LogInformation($"tracId: {traceId}");
//            //Logger.LogInformation($"user: {user}");

//            // Log Controller name and action
//            var controllerActionDescriptor = context
//                .GetEndpoint()
//                .Metadata
//                .GetMetadata<ControllerActionDescriptor>();
//            controllerName = controllerActionDescriptor.ControllerName;
//            actionName = controllerActionDescriptor.ActionName;
//            //Logger.LogInformation($"Controller: {controllerName}");
//            //Logger.LogInformation($"Action: {actionName}");
//            // Log Controller name and action

//            // Log request header
//            if (context.Request != null && context.Request.Headers != null && context.Request.Headers.ContainsKey("User-Agent"))
//            {
//                userAgent = context.Request.Headers["User-Agent"];
//            }
//            var requestHeader = JsonSerializer.Serialize(context.Request?.Headers);
//            //Logger.LogInformation($"Request-Header: {requestHeader}");
//            // Log request header

//            // Log request body
//            context.Request.EnableBuffering();
//            context.Request.Body.Seek(0, SeekOrigin.Begin);
//            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
//            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
//            var bodyAsText = Encoding.UTF8.GetString(buffer);
//            context.Request.Body.Seek(0, SeekOrigin.Begin);
//            //Logger.LogInformation($"Request-Body: {bodyAsText}");
//            // Log request body

//            var originResponseBody = context.Response.Body;
//            using var responsBody = new MemoryStream();
//            context.Response.Body = responsBody;

//            await Next(context);

//            // Log response header
//            var responseHeader = JsonSerializer.Serialize(context.Response?.Headers);
//            //Logger.LogInformation($"Response-Header: {responseHeader}");
//            // Log response header

//            // Log response body
//            context.Response.Body.Seek(0, SeekOrigin.Begin);
//            string text = await new StreamReader(context.Response.Body).ReadToEndAsync();
//            //Logger.LogInformation($"Response-Body: {text}");
//            context.Response.Body.Seek(0, SeekOrigin.Begin);
//            // Log response body

//            var actionAudit = new ActionAudit
//            {
//                UserName = userName,
//                Controller = controllerName,
//                Action = actionName,
//                Parameter = query,
//                UserAgent = userAgent,
//                ObjectId = objectId,
//                NewObjectValue = bodyAsText,
//                Ip = ip,
//                BeginAuditTime = beginTime,
//                EndAuditTime = DateTime.UtcNow,
//                TraceId = traceId
//            };

//            await dbContext.ActionAudit.AddAsync(actionAudit);
//            //throw new Exception("Cannot write DataBase");
//            await dbContext.SaveChangesAsync();

//            await responsBody.CopyToAsync(originResponseBody);

//            //}
//            //catch (Exception e)
//            //{
//            //    Logger.LogError($"Exception: {e.Message}\n{e.StackTrace}");
//            //}
//        }
//    }
//}
