using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.Report
{
    public class LocalProductViewModel
    {
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
        public string SupNum { get; set; }
        public string SupName { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string CreateName { get; set; }
        public string Remark { get; set; }
        public int? InvalidNum { get; set; }
        public string ValidityDateTime { get; set; }
        public string Batch { get; set; }
        public double? InPrice { get; set; }
        public double? OutPrice { get; set; }
        public int CompanyId { get; set; }
        public string Status { get; set; }
    }
}
