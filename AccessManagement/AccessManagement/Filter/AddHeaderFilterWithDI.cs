using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMSData;

namespace AccessManagement.Filter
{
    public class AddHeaderFilterWithDI : ActionFilterAttribute
    {
        public AddHeaderFilterWithDI(LuJCDBContext db)
        {

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //context.HttpContext.Response.Headers.Add(
            //    _name, new string[] { _value });
            //base.OnResultExecuting(context);
        }
    }
}
