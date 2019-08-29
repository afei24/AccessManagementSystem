using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.IMS
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string CateNum { get; set; }
        [Display(Name ="名称")]
        public string CateName { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        public int CompanyId { get; set; }
    }
}
