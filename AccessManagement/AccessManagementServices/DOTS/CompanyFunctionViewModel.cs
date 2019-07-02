using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS
{
    [Serializable]
    public class CompanyFunctionViewModel
    {
        public CompanyFunctionViewModel()
        {
            FunctionSelecteds = new List<FunctionSelected>();
        }
        [Display(Name = "功能名称")]
        public string Name { get; set; }

        [Display(Name = "编码")]
        public string Code { get; set; }

        public string AppMenuName { get; set; }
        public string ParentAppMenuName { get; set; }
        public  List<FunctionSelected> FunctionSelecteds { get; set; }
    }

    [Serializable]
    public class FunctionSelected
    {
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
