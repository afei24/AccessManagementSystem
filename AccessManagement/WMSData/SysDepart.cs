using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class SysDepart
    {
        public int Id { get; set; }
        public string DepartNum { get; set; }
        public string DepartName { get; set; }
        public int ChildCount { get; set; }
        public string ParentNum { get; set; }
        public int Depth { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
