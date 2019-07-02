using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Middleware
{
    public static class AccountSessionExtensions
    {
        public static IApplicationBuilder UseAccountSession(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AccountSessionMiddleware>();
        }
    }
}
