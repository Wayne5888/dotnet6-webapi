using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class LogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogMiddleware> _logger;

    public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        DateTime requestTime = DateTime.UtcNow;

        _logger.LogInformation($"Request {context.Request.Method}: {context.Request.Path} at {requestTime}");

        string requestBody = await ReadRequestBody(context.Request);
        _logger.LogInformation($"Request Body: {requestBody}");

        var originalBodyStream = context.Response.Body;
        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;
            await _next(context);
            DateTime responseTime = DateTime.UtcNow;
            _logger.LogInformation($"Response: {context.Response.StatusCode} at {responseTime}");
            responseBody.Seek(0, SeekOrigin.Begin);
            string responseBodyContent = await new StreamReader(responseBody).ReadToEndAsync();
            _logger.LogInformation($"Response Body: {responseBodyContent}");
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();
        using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
        {
            string requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return requestBody;
        }
    }

}