using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagementServices.Models
{
    public class ResponseModel<T>
    {
        public int status { get; set; }
        public string message { get; set; }
        public int total { get; set; }
        public List<T> data { get; set; }
    }
}
