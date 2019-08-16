using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class InStorUploadDetail
    {
        public long Id { get; set; }
        public string OrderNum { get; set; }
        public string SnNum { get; set; }
        public string BarCode { get; set; }
        public double Num { get; set; }
        public double RealNum { get; set; }
        public string LocalNum { get; set; }
        public string StorageNum { get; set; }
        public string ProductNum { get; set; }
        public string ProductName { get; set; }
        public int Status { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public double? PutRealNum { get; set; }
    }
}
