using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AccessManagement.Filter
{
    /// <summary>
    /// 全局异常类
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        ILogger<HttpGlobalExceptionFilter> _logger;
        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.Message, context.Exception);
        }
    }
}
