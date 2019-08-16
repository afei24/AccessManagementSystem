using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class CheckData
    {
        public int Id { get; set; }
        public string OrderNum { get; set; }
        public string LocalNum { get; set; }
        public string LocalName { get; set; }
        public string StorageNum { get; set; }
        public string ProductNum { get; set; }
        public string BarCode { get; set; }
        public string ProductName { get; set; }
        public string BatchNum { get; set; }
        public double LocalQty { get; set; }
        public double FirstQty { get; set; }
        public double SecondQty { get; set; }
        public double DifQty { get; set; }
        public string FirstUser { get; set; }
        public string SecondUser { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
