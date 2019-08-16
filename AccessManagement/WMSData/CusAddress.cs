using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class CusAddress
    {
        public int Id { get; set; }
        public string SnNum { get; set; }
        public string CusNum { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string Remark { get; set; }
    }
}
