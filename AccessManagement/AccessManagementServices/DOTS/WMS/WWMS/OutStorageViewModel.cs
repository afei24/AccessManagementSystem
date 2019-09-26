using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.WWMS
{
    public class OutStorageViewModel
    {
        public OutStorageViewModel()
        {
            Customers = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string MergeOrderNum { get; set; }
        public string OrderNum { get; set; }
        [Display(Name = "出库单类型")]
        public int OutType { get; set; }
        public int ProductType { get; set; }
        public string StorageNum { get; set; }
        [Display(Name = "供应商")]
        public string SupNum { get; set; }
        public string SupName { get; set; }
        [Display(Name = "客户")]
        public string CusNum { get; set; }
        public string CusName { get; set; }
        public string Contact { get; set; }
        [Display(Name = "电话")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ContractOrder { get; set; }
        public double Num { get; set; }
        public double RealNum { get; set; }
        public double PutNum { get; set; }
        public double Amount { get; set; }
        public double Weight { get; set; }
        public DateTime? SendDate { get; set; }
        public int Status { get; set; }
        public string StatusStr { get; set; }
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
        public string CusType { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public string CreateTimeStr { get; set; }

        public double? Price { get; set; }
    }
}
