using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS
{
    public class RoleViewModel
    {
        [Display(Name = "角色ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "角色名称")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "描述")]
        public string Description { get; set; }

        [Display(Name = "所属企业")]
        public int CompanyId { get; set; }
        public List<FunctionViewModel> Functions { get; set; }
    }
}
