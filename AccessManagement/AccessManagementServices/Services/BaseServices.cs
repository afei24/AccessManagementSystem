using AccessManagementServices.DOTS;
using AccessManagementServices.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.Services
{
    public class BaseServices
    {
        protected ILogger _logger;
        public BaseServices(ILogger<BaseServices> logger)
        {
            _logger = logger;
        }
    }
}
