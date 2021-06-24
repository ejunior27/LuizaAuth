using Domain.LuizaAuth.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Domain.LuizaAuth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api.LuizaAuth.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex is BusinessException)
                {
                    LogTrace(ex);
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    await SerializeErrorAsync(httpContext, new { message = ex.Message, error = ex.GetType().Name });
                    return;
                }

                logger.LogError("Intercepted by ExceptionMiddleware: " + ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;

            return context.Response.WriteAsync(new ErrorResponseDto(exception.Message).ToString());
        }

        private async Task SerializeErrorAsync(HttpContext httpContext, object error)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var json = JsonConvert.SerializeObject(error, serializerSettings);
            var bytes = Encoding.UTF8.GetBytes(json);
            await httpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }

        private void LogTrace(Exception ex)
        {
            logger.LogTrace($"Exception message: {ex.Message}");
            var internalError = ex.InnerException;
            while (internalError != null)
            {
                logger.LogTrace($"InnerException message: {internalError.Message}");
                internalError = internalError.InnerException;
            }

            logger.LogTrace($"Source: {ex.Source}");
        }
    }
}
