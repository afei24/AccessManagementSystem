﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.Filters
{
    public class CheckStockFilters : BaseFilters
    {
        public string OrderNum { get; set; }
        public string Code { get; set; }
    }
}
