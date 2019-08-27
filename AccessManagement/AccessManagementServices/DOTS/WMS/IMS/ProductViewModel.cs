using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.IMS
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string SnNum { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string CommodityCode { get; set; }
        public string BarCode { get; set; }
        public string ProductName { get; set; }
        public string Sx1 { get; set; }
        public string Sx2 { get; set; }
        public string CateNum { get; set; }
        public string CateName { get; set; }
        public double Num { get; set; }
        public double MinNum { get; set; }
        public double MaxNum { get; set; }
        public string UnitNum { get; set; }
        public string UnitName { get; set; }
        public double InPrice { get; set; }
        public double OutPrice { get; set; }
        public double AvgPrice { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string StorageNum { get; set; }
        public string DefaultLocal { get; set; }
        public string CusNum { get; set; }
        public string CusName { get; set; }
        public string Display { get; set; }
        public string Remark { get; set; }
        public int? CanDepositNum { get; set; }
        public string ValidityDays { get; set; }
        public string VolumeSize { get; set; }
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
    }
}
