using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class ReportParams
    {
        public int Id { get; set; }
        public string ParamNum { get; set; }
        public string ReportNum { get; set; }
        public string InputNo { get; set; }
        public string ParamName { get; set; }
        public string ShowName { get; set; }
        public string ParamType { get; set; }
        public string ParamData { get; set; }
        public string DefaultValue { get; set; }
        public string ParamElement { get; set; }
        public string Remark { get; set; }
    }
}
