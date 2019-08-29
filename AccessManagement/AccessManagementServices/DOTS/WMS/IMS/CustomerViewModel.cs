using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.IMS
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Display(Name ="客户编号")]
        public string CusNum { get; set; }
        [Display(Name = "客户名称")]
        [Required]
        public string CusName { get; set; }
        [Display(Name = "电话")]
        public string Phone { get; set; }
        [Display(Name = "邮件")]
        public string Email { get; set; }
        [Display(Name = "传真")]
        public string Fax { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        public int? CusType { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        public int CompanyId { get; set; }
    }
}
