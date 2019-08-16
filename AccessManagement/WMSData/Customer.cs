using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string CusNum { get; set; }
        public string CusName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public int? CusType { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string Remark { get; set; }
        public int CompanyId { get; set; }
    }
}
