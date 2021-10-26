using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.ServicesLayer.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            LogLevel logLevel = LogLevel.Trace;
            string log = "";

            try
            {
                log = $"Request:{context.Request.Method}, {context.Request.Path},  {await ReadRequestBody(context)}";

                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                log += ", " + ex.Message;
                logLevel = LogLevel.Error;
                throw;
            }
            finally
            {
                log += $", Response:{context.Response.StatusCode}";

                _logger.Log(logLevel, log);
            }
        }

        private async Task<string> ReadRequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();

            using (var bodyReader = new StreamReader(stream: context.Request.Body,
                                                      encoding: Encoding.UTF8,
                                                      detectEncodingFromByteOrderMarks: false,
                                                      bufferSize: 1024,
                                                      leaveOpen: true))
            {
                var body = await bodyReader.ReadToEndAsync();

                context.Request.Body.Position = 0;

                return body;
            }
        }
    }

    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
