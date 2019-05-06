using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        [Display(Name ="账号")]
        [Required]
        public string Account { get; set; }
        [Display(Name = "名称")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "密码")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "省")]
        public string Province { get; set; }
        [Display(Name = "市")]
        public string City { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "电话")]
        public string Phone { get; set; }
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "创建人")]
        public string CreateUser { get; set; }
        [Display(Name = "更新时间")]
        public DateTime? UpdateTime { get; set; }
        [Display(Name = "更新人")]
        public string UpdateUser { get; set; }
        [Display(Name = "所属企业")]
        public int CompanyId { get; set; }
        public List<FunctionViewModel> Functions { get; set; }
    }
}
