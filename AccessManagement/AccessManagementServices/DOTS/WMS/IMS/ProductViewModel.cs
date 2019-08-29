using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.IMS
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Branchs = new List<SelectListItem>();
            Goods = new List<SelectListItem>();
            Customers = new List<SelectListItem>();
            Measures = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string SnNum { get; set; }
        public string Color { get; set; }
        [Display(Name = "规格")]
        public string Size { get; set; }
        public string CommodityCode { get; set; }
        [Display(Name ="条码")]
        public string BarCode { get; set; }
        [Display(Name = "名称")]
        public string ProductName { get; set; }
        public string Sx1 { get; set; }
        public string Sx2 { get; set; }
        public string CateNum { get; set; }
        [Display(Name = "分类")]
        public string CateName { get; set; }
        [Display(Name = "数量")]
        public double Num { get; set; }
        [Display(Name = "下限")]
        public double MinNum { get; set; }
        [Display(Name = "上限")]
        public double MaxNum { get; set; }
        
        public string UnitNum { get; set; }
        [Display(Name = "单位")]
        public string UnitName { get; set; }
        [Display(Name = "入库价")]
        public double InPrice { get; set; }
        [Display(Name = "出库价")]
        public double OutPrice { get; set; }
        [Display(Name = "单价")]
        public double AvgPrice { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        [Display(Name = "默认仓库")]
        public string StorageNum { get; set; }
        [Display(Name = "默认库位")]
        public string DefaultLocal { get; set; }
        public string CusNum { get; set; }
        [Display(Name = "客户")]
        public string CusName { get; set; }
        [Display(Name = "条码")]
        public string Display { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Display(Name = "可存放数量")]
        public int? CanDepositNum { get; set; }
        [Display(Name = "有效天数")]
        public string ValidityDays { get; set; }
        [Display(Name = "长宽高")]
        public string VolumeSize { get; set; }
        [Display(Name = "体积")]
        public string Volume { get; set; }
        public string Batch { get; set; }
        public int CompanyId { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public double LocalProductNum { get; set; }

        /// <summary>
        /// 库位
        /// </summary>
        public string LocalName { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        public double InStorageNum { get; set; }

        /// <summary>
        /// 入库数量所占比例
        /// </summary>
        public double InStorageNumPCT { get; set; }

        /// <summary>
        /// 出库数量
        /// </summary>
        public double OutStorageNum { get; set; }

        /// <summary>
        /// 出库数量所占比例
        /// </summary>
        public double OutStorageNumPCT { get; set; }

        /// <summary>
        /// 报损数量
        /// </summary>
        public double BadReportNum { get; set; }

        /// <summary>
        /// 总计库存数量
        /// </summary>
        public double TotalLocalProductNum { get; set; }

        /// <summary>
        /// 总计入库数量
        /// </summary>
        public double TotalInStorageNum { get; set; }

        /// <summary>
        /// 总计出库数量
        /// </summary>
        public double TotalOutStorageNum { get; set; }

        /// <summary>
        /// 总计报损数量
        /// </summary>
        public double TotalBadReportNum { get; set; }

        public List<SelectListItem> Branchs { get; set; }
        public List<SelectListItem> Goods { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> Measures { get; set; }
    }
}
