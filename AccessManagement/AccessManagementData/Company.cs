using AccessManagementServices.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementData
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public ComapnyStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
