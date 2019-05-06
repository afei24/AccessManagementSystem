using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS
{
    public class PresetFunctionViewModel
    {
        [Display(Name = "功能ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "功能名称")]
        public string Name { get; set; }

        [Display(Name = "所属企业ID")]
        public int CompanyId { get; set; }
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
        [Required]
        [StringLength(64)]
        public string Code { get; set; }

        [StringLength(256)]
        [Display(Name = "功能描述")]
        public string Description { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称1")]
        public string OpName1 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称2")]
        public string OpName2 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称3")]
        public string OpName3 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称4")]
        public string OpName4 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称5")]
        public string OpName5 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称6")]
        public string OpName6 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称7")]
        public string OpName7 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称8")]
        public string OpName8 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称9")]
        public string OpName9 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称10")]
        public string OpName10 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称11")]
        public string OpName11 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称12")]
        public string OpName12 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称13")]
        public string OpName13 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称14")]
        public string OpName14 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称15")]
        public string OpName15 { get; set; }

        [StringLength(32)]
        [Display(Name = "操作指令名称16")]
        public string OpName16 { get; set; }


        [Display(Name = "所属菜单")]
        public string MenuName { get; set; }
    }
}
