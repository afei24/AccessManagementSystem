using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.Filters
{
    public class OutStorageFilters : BaseFilters
    {
        public string OrderNum { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
    }
}
