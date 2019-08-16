using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.DOTS
{
    [Serializable]
    public class LocationViewModel
    {
        public int Id { get; set; }
        public string LocalNum { get; set; }
        public string LocalBarCode { get; set; }
        public string LocalName { get; set; }
        public string StorageNum { get; set; }
        public int StorageType { get; set; }
        public int LocalType { get; set; }
        public string UnitNum { get; set; }
        public string UnitName { get; set; }
        public string Remark { get; set; }
        public int IsForbid { get; set; }
        public int IsDefault { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
