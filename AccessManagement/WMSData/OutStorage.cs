using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class OutStorage
    {
        public int Id { get; set; }
        public string MergeOrderNum { get; set; }
        public string OrderNum { get; set; }
        public int OutType { get; set; }
        public int ProductType { get; set; }
        public string StorageNum { get; set; }
        public string SupNum { get; set; }
        public string SupName { get; set; }
        public string CusNum { get; set; }
        public string CusName { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ContractOrder { get; set; }
        public double Num { get; set; }
        public double Amount { get; set; }
        public double Weight { get; set; }
        public DateTime? SendDate { get; set; }
        public int Status { get; set; }
        public int IsDelete { get; set; }
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
    }
}
