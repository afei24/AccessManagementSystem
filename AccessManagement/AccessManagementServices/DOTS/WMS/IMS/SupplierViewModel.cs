using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.IMS
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        public string SupNum { get; set; }
        [Display(Name ="供应商名称")]
        public string SupName { get; set; }
        [Display(Name = "供应商类型")]
        public int? SupType { get; set; }
        public string SupTypeStr { get; set; }
        [Display(Name = "电话")]
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        [Display(Name = "联系人")]
        public string ContactName { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        public string CreateUser { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "联系电话")]
        public string ContractNum { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public int CompanyId { get; set; }
    }
}
