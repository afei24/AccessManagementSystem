using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class Storage
    {
        public int Id { get; set; }
        public string StorageNum { get; set; }
        public string StorageName { get; set; }
        public int StorageType { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Action { get; set; }
        public int IsDelete { get; set; }
        public int Status { get; set; }
        public int IsForbid { get; set; }
        public int IsDefault { get; set; }
        public DateTime CreateTime { get; set; }
        public string Remark { get; set; }
    }
}
