using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class CheckStock
    {
        public int Id { get; set; }
        public string OrderNum { get; set; }
        public int Type { get; set; }
        public int ProductType { get; set; }
        public string StorageNum { get; set; }
        public string ContractOrder { get; set; }
        public int Status { get; set; }
        public double LocalQty { get; set; }
        public double CheckQty { get; set; }
        public int IsDelete { get; set; }
        public int IsComplete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string AuditUser { get; set; }
        public DateTime? AuditeTime { get; set; }
        public string PrintUser { get; set; }
        public DateTime? PrintTime { get; set; }
        public string Reason { get; set; }
        public int OperateType { get; set; }
        public string EquipmentNum { get; set; }
        public string EquipmentCode { get; set; }
        public string Remark { get; set; }
        public int CompanyId { get; set; }
    }
}
