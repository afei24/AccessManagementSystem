using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class Orders
    {
        public int Id { get; set; }
        public string SnNum { get; set; }
        public string OrderNum { get; set; }
        public int OrderType { get; set; }
        public string CusNum { get; set; }
        public string CusName { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ContractOrder { get; set; }
        public double? Num { get; set; }
        public double? Amount { get; set; }
        public double? Weight { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime SendDate { get; set; }
        public int AuditeStatus { get; set; }
        public int Status { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
    }
}
