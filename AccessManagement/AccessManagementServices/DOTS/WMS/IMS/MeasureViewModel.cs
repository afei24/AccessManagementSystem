using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.IMS
{
    public class MeasureViewModel
    {
        public int Id { get; set; }
        public string Sn { get; set; }
        public string MeasureNum { get; set; }
        [Display(Name ="名称")]
        public string MeasureName { get; set; }
        public int CompanyId { get; set; }
    }
}
