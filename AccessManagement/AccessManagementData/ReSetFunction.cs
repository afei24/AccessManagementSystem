using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementData
{
    public class ReSetFunction
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 类型：
        ///0: 系统预置
        ///1: 用户自定义
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 权限检查程序使用的代码
        /// </summary>
        public string Code { get; set; }

        public string Description { get; set; }

        public string OpName1 { get; set; }

        public string OpName2 { get; set; }

        public string OpName3 { get; set; }

        public string OpName4 { get; set; }

        public string OpName5 { get; set; }

        public string OpName6 { get; set; }

        public string OpName7 { get; set; }

        public string OpName8 { get; set; }

        public string OpName9 { get; set; }

        public string OpName10 { get; set; }
    }
}
