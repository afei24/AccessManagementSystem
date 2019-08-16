using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class ProductCategory
    {
        public int Id { get; set; }
        public string CateNum { get; set; }
        public string CateName { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string Remark { get; set; }
    }
}
