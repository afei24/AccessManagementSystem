using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS
{
    public class AppMenuViewModel
    {
        public int Id { get; set; }
        [Display(Name ="名称")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "路由")]
        [Required]
        public string Route { get; set; }
        [Display(Name = "顺序")]
        public int Order { get; set; }
        [Required]
        [Display(Name = "编码")]
        public string Code { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "父菜单")]
        public int? ParentId { get; set; }
        public IList<SelectListItem> ParentAppMenus { get; set; }
    }
}
