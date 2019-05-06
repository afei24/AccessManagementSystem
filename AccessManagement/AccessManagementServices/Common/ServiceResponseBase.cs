using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.Common
{
    public enum Status { ok, error, ignore, httpNotFound = 404 }

    public class ServiceResponseBase
    {
        public Status Status { get; set; }
        public string Message { get; set; }
    }
}
