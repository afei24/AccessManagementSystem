using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class Reports
    {
        public int Id { get; set; }
        public string ReportNum { get; set; }
        public string ReportName { get; set; }
        public int? ReportType { get; set; }
        public string Remark { get; set; }
        public string DataSource { get; set; }
        public int? DsType { get; set; }
        public string FileName { get; set; }
        public int? IsDelete { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
