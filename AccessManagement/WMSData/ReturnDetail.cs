using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class ReturnDetail
    {
        public int Id { get; set; }
        public string SnNum { get; set; }
        public string OrderNum { get; set; }
        public string ContractOrder { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string ProductNum { get; set; }
        public string BatchNum { get; set; }
        public string LocalNum { get; set; }
        public string StorageNum { get; set; }
        public double Num { get; set; }
        public double? OutPrice { get; set; }
        public double? Amount { get; set; }
        public double BackNum { get; set; }
        public double BackAmount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
