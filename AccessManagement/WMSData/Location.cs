using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class Location
    {
        public int Id { get; set; }
        public string LocalNum { get; set; }
        public string LocalBarCode { get; set; }
        public string LocalName { get; set; }
        public string StorageNum { get; set; }
        public int StorageType { get; set; }
        public int LocalType { get; set; }
        public string Rack { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
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
