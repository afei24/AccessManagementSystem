using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class Equipment
    {
        public int Id { get; set; }
        public string SnNum { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentNum { get; set; }
        public int IsImpower { get; set; }
        public string Flag { get; set; }
        public int IsDelete { get; set; }
        public int Status { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public string Remark { get; set; }
    }
}
