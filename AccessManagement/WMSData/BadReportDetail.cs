using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class BadReportDetail
    {
        public int Id { get; set; }
        public string SnNum { get; set; }
        public string OrderNum { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string ProductNum { get; set; }
        public string BatchNum { get; set; }
        public double Num { get; set; }
        public double? InPrice { get; set; }
        public double? Amount { get; set; }
        public DateTime CreateTime { get; set; }
        public string StorageNum { get; set; }
        public string FromLocalNum { get; set; }
        public string ToLocalNum { get; set; }
        public string Remark { get; set; }
        public string ImageUrl { get; set; }
    }
}
