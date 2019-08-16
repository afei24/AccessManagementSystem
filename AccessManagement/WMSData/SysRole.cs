using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class SysRole
    {
        public int Id { get; set; }
        public string RoleNum { get; set; }
        public string RoleName { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string Remark { get; set; }
    }
}
