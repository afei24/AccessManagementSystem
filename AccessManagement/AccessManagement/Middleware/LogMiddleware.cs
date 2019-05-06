using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Middleware
{
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
            var requestPath = context.Request.Path.ToString();
            var requestMethod = context.Request.Method.ToString();
            var requestHost = context.Request.Host.ToString();
            var requestHeaders = context.Request.Headers.ToString();
            //_logger.LogInformation("Request Path:" + requestPath + Environment.NewLine
            //    + " requestMethod:" + requestMethod + " requestHost:" + requestHost + Environment.NewLine
            //    + " requestHeaders:" + requestHeaders);
            _logger.LogDebug("Request Path:" + requestPath + Environment.NewLine
                + " requestMethod:" + requestMethod + " requestHost:" + requestHost + Environment.NewLine
                + " requestHeaders:" + requestHeaders);
            await _next.Invoke(context);
            _logger.LogDebug("Response Path:" + requestPath + Environment.NewLine
                + "Body:" + context.Response.Body.ToString());

        }
    }
}
