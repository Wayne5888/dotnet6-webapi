using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using webapi.CustomException;
using Microsoft.Extensions.Logging;

namespace webapi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;


        public ExceptionMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ApiException ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = ex.StatusCode;
                var body = new {
                    httpCode = ex.StatusCode,
                    message = ex.Message
                };
                var result = JsonConvert.SerializeObject(body);
                await httpContext.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandle Exception message: {ex.Message}");
                _logger.LogError($"Unhandle Exception stack: {ex}");
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 500;

                var body = new {
                    httpCode = 500,
                    message = "Internal Server Error, Please Try Again Later",
                };
                var result = JsonConvert.SerializeObject(body);
                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}
