using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class InventoryBook
    {
        public int Id { get; set; }
        public string ProductNum { get; set; }
        public string BarCode { get; set; }
        public string ProductName { get; set; }
        public string BatchNum { get; set; }
        public double Num { get; set; }
        public int Type { get; set; }
        public string ContactOrder { get; set; }
        public string FromLocalNum { get; set; }
        public string ToLocalNum { get; set; }
        public string StoreNum { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
    }
}
