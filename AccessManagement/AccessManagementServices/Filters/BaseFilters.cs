using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagementServices.Filters
{
    public class BaseFilters
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
