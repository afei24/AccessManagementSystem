using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class Measure
    {
        public int Id { get; set; }
        public string Sn { get; set; }
        public string MeasureNum { get; set; }
        public string MeasureName { get; set; }
        public int CompanyId { get; set; }
    }
}
