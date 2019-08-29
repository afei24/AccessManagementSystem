using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS
{
    [Serializable]
    public class LocationViewModel
    {
        public LocationViewModel()
        {
            Branchs = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string LocalNum { get; set; }
        [Display(Name ="库位条码")]
        public string LocalBarCode { get; set; }
        [Display(Name = "库位名称")]
        public string LocalName { get; set; }
        [Display(Name = "条码")]
        public string StorageNum { get; set; }
        public int StorageType { get; set; }
        [Display(Name = "库位类型")]
        public int LocalType { get; set; }
        public string LocalTypeStr { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        public int IsForbid { get; set; }
        public int IsDefault { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public int CompanyId { get; set; }
        [Display(Name = "仓库名称")]
        public int BranchId { get; set; }
        public List<SelectListItem> Branchs { get; set; }
    }
}
