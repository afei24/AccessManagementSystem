using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class SysResource
    {
        public int Id { get; set; }
        public string ResNum { get; set; }
        public string ResName { get; set; }
        public string ParentNum { get; set; }
        public int Depth { get; set; }
        public string ParentPath { get; set; }
        public int ChildCount { get; set; }
        public int Sort { get; set; }
        public short IsHide { get; set; }
        public short IsDelete { get; set; }
        public string Url { get; set; }
        public string CssName { get; set; }
        public DateTime CreateTime { get; set; }
        public short Depart { get; set; }
        public short ResType { get; set; }
        public DateTime UpdateTime { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public string CreateIp { get; set; }
        public string UpdateIp { get; set; }
        public string Remark { get; set; }
    }
}
