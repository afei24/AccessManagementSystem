using AccessManagementServices.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS
{
    [Serializable]
    public class AccountViewModel
    {
        public AccountViewModel()
        {
            FunctionViewModels = new List<FunctionViewModel>();
            PresetFunctionViewModel = new List<PresetFunctionViewModel>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "请填写账号")]
        [StringLength(100)]
        [Display(Name = "账号")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "请填写密码")]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Required(ErrorMessage = "请填写姓名")]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "状态")]
        public string StatusName { get; set; }
        [Required(ErrorMessage = "请填写城市")]
        [Display(Name = "城市")]
        public string City { get; set; }
        [Display(Name = "手机")]
        [Phone]
        public string Phone { get; set; }
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "创建人")]
        public int CreateUserId { get; set; }
        [Display(Name = "公司")]
        public int CompanyId { get; set; }
        [Required]
        [Display(Name = "网点")]
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string CreateTimeStr { get; set; }
        public bool RememberMe { get; set; }
        [Required]
        [Display(Name = "角色")]
        public string RoleId { get; set; }
        public List<FunctionViewModel> FunctionViewModels { get; set; }
        public List<PresetFunctionViewModel> PresetFunctionViewModel { get; set; }
    }
}
