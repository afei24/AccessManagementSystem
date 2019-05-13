using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccessManagementServices.DOTS
{
    public class BranchViewModel
    {
        public BranchViewModel()
        {
            ParentBranchs = new List<BranchViewModel>();
        }
        public int Id { get; set; }

        public int? ParentBranchId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "机构名称")]
        public string Name { get; set; }

        [Display(Name = "类型")]
        public int Type { get; set; }

        [Required]
        [Display(Name = "城市")]
        public string City { get; set; }

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
        [Display(Name = "父机构")]
        public ICollection<BranchViewModel> ParentBranchs { get; set; }
        [Display(Name = "父机构")]
        public string ParentBranchName { get; set; }
    }
}
