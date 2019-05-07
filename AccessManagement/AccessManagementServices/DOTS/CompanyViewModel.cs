using AccessManagementServices.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            Functions = new List<FunctionViewModel>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "公司名称")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "联系电话")]
        [RegularExpression(@"^\s*(((1\d{10})|(\d{3,4}-\d{7,8})(-\d{1,4}){0,1})[\s,，]+)*((1\d{10})|(\d{3,4}-\d{7,8})(-\d{1,4}){0,1})\s*$", ErrorMessage = "请输入正确格式，如有多个请用逗号隔开")]
        public string Tel { get; set; }

        [Display(Name = "状态")]
        public ComapnyStatus Status { get; set; }

        [Display(Name = "创建时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "更新时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? UpdateTime { get; set; }
        public List<FunctionViewModel> Functions { get; set; }
        public IList<SelectListItem> ComapnyStatuss { get; set; }
    }
}
