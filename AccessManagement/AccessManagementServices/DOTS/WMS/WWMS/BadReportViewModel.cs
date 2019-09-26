using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.WWMS
{
    public class BadReportViewModel
    {
        public int Id { get; set; }
        public string OrderNum { get; set; }
        [Display(Name ="报损类型")]
        public int BadType { get; set; }
        public string BadTypeStr { get; set; }
        public int ProductType { get; set; }
        public string StorageNum { get; set; }
        public string ContractOrder { get; set; }
        public int Status { get; set; }
        public string StatusStr { get; set; }
        public double? Num { get; set; }
        public double? Amount { get; set; }
        public double? Weight { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string AuditUser { get; set; }
        public DateTime? AuditeTime { get; set; }
        public string PrintUser { get; set; }
        public DateTime? PrintTime { get; set; }
        public string Reason { get; set; }
        public int OperateType { get; set; }
        public string EquipmentNum { get; set; }
        public string EquipmentCode { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        public int CompanyId { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string StatusLable { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 条码编号
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNum { get; set; }

        public double NumPCT { get; set; }
        public string CreateTimeStr { get; set; }
    }
}
