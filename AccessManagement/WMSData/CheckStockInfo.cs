using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class CheckStockInfo
    {
        public int Id { get; set; }
        public string OrderNum { get; set; }
        public string StorageNum { get; set; }
        public string TargetNum { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
