using AccessManagementData;
using AccessManagementServices.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Filter
{
    public class AccessAttribute : ActionFilterAttribute
    {
        private readonly FunctionCode _functionCode;

        public AccessAttribute(FunctionCode functionCode)
        {
            _functionCode = functionCode;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            byte[] value = null;
            if (context.HttpContext.Session.TryGetValue("functions",out value))
            {
                var functions = (List<Function>)SerializeHelper.DeserializeWithBinary(value);
                if (!functions.Any(o => o.Code.Contains(_functionCode.ToString())))
                {
                    context.Result = new ContentResult()
                    {
                        Content = "请设置权限"
                    };
                    context.HttpContext.Response.Redirect("/Home/Index");
                }
            }
            else
            {
                context.Result = new ContentResult()
                {
                    Content = "请设置权限"
                };
                context.HttpContext.Response.Redirect("/Home/Index");
            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //context.HttpContext.Response.Headers.Add(
            //    _name, new string[] { _value });
            //base.OnResultExecuting(context);
        }
    }
}
