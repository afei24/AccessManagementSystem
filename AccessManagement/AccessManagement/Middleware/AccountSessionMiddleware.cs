using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Middleware
{
    public class AccountSessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AccountSessionMiddleware> _logger;
        public AccountSessionMiddleware(RequestDelegate next, ILogger<AccountSessionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestPath = context.Request.Path.ToString();
            if (!requestPath.Contains("api"))
            {
                if (context.Session.Get("account") != null)
                {
                    var account = (AccountViewModel)SerializeHelper.DeserializeWithBinary(context.Session.Get("account"));
                    _logger.LogInformation("login name:" + account.AccountName + " password:" + account.Password);
                    
                }
                else if (!requestPath.Contains("Account/Login"))
                {
                    context.Response.Redirect("/Account/Login");
                }
            }

            await _next.Invoke(context);

        }
    }
}
