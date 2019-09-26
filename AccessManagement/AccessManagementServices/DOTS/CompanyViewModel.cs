using AccessManagementServices.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS
{
    [Serializable]
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            CompanyFunctionViewModels = new List<CompanyFunctionViewModel>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "请填写公司名称")]
        [Display(Name = "公司名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请填写联系电话")]
        [StringLength(50)]
        [Display(Name = "联系电话")]
        [RegularExpression(@"^\s*(((1\d{10})|(\d{3,4}-\d{7,8})(-\d{1,4}){0,1})[\s,，]+)*((1\d{10})|(\d{3,4}-\d{7,8})(-\d{1,4}){0,1})\s*$", ErrorMessage = "请输入正确格式，如有多个请用逗号隔开")]
        public string Tel { get; set; }

        [Display(Name = "状态")]
        public string StatusName { get; set; }
        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "创建时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "更新时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? UpdateTime { get; set; }
        public IList<SelectListItem> ComapnyStatuss { get; set; }
        public List<CompanyFunctionViewModel> CompanyFunctionViewModels { get; set; }
    }
}
