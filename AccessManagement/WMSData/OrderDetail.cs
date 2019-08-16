using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public string SnNum { get; set; }
        public string OrderSnNum { get; set; }
        public string OrderNum { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string ProductNum { get; set; }
        public double Num { get; set; }
        public double RealNum { get; set; }
        public string UnitNum { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public int? Status { get; set; }
        public DateTime? SendTime { get; set; }
        public string ContractId { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
