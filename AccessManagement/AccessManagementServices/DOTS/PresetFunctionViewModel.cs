using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS
{
    public class PresetFunctionViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "功能名称")]
        public string Name { get; set; }

        /// <summary>
        /// 类型：
        ///0: 系统预置
        ///1: 用户自定义
        /// </summary>
        [Display(Name = "类型")]
        public int Type { get; set; }
        /// <summary>
        /// 权限检查程序使用的代码
        /// </summary>
        [Required(ErrorMessage = "请填写编码")]
        [StringLength(64)]
        [Display(Name = "编码")]
        public string Code { get; set; }

        [StringLength(256)]
        [Display(Name = "描述")]
        public string Description { get; set; }

        [StringLength(32)]
        [Display(Name = "功能1")]
        public string OpName1 { get; set; }

        [StringLength(32)]
        [Display(Name = "功能2")]
        public string OpName2 { get; set; }

        [StringLength(32)]
        [Display(Name = "功能3")]
        public string OpName3 { get; set; }

        [StringLength(32)]
        [Display(Name = "功能4")]
        public string OpName4 { get; set; }

        [StringLength(32)]
        [Display(Name = "功能5")]
        public string OpName5 { get; set; }

        [StringLength(32)]
        [Display(Name = "功能6")]
        public string OpName6 { get; set; }

        [StringLength(32)]
        [Display(Name = "功能7")]
        public string OpName7 { get; set; }

        [StringLength(32)]
        [Display(Name = "功能8")]
        public string OpName8 { get; set; }

        [StringLength(32)]
        [Display(Name = "功能9")]
        public string OpName9 { get; set; }

        [StringLength(32)]
        [Display(Name = "功能10")]
        public string OpName10 { get; set; }


    }
}
