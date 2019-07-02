using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagementServices.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace AccessManagement.Controllers
{
    public class BaseController : Controller
    {
        protected ILogger<BaseController> _logger;
        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        public SortCol GetSort()
        {
            var sortCol = new SortCol() {
                Field = HttpContext.Request.Query["field"],
                Type = HttpContext.Request.Query["type"],
            };

            return sortCol;
        }
    }
}