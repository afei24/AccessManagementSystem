using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class CloneTemp
    {
        public int CloneId { get; set; }
        public string OrderNum { get; set; }
        public int Id { get; set; }
        public string Sn { get; set; }
        public string StorageNum { get; set; }
        public string StorageName { get; set; }
        public string LocalNum { get; set; }
        public string LocalName { get; set; }
        public int? LocalType { get; set; }
        public string ProductNum { get; set; }
        public string BarCode { get; set; }
        public string ProductName { get; set; }
        public string BatchNum { get; set; }
        public double Num { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string CreateName { get; set; }
        public string Remark { get; set; }
    }
}
