using AccessManagementServices.Common;
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

        [Required]
        [StringLength(100)]
        [Display(Name = "账号")]
        public string AccountName { get; set; }

        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "类型")]
        public int Type { get; set; }
        public string StatusName { get; set; }
        [Required]
        [Display(Name = "城市")]
        public string City { get; set; }
        [Required]
        [Display(Name = "手机")]
        public string Phone { get; set; }
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "创建人")]
        public int CreateUserId { get; set; }
        [Display(Name = "公司")]
        public int CompanyId { get; set; }
        [Display(Name = "网点")]
        public int BranchId { get; set; }
        public bool RememberMe { get; set; }
        public List<FunctionViewModel> FunctionViewModels { get; set; }
        public List<PresetFunctionViewModel> PresetFunctionViewModel { get; set; }
    }
}
