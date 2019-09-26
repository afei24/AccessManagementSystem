using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.WWMS
{
    public class InStorageViewModel
    {
        public int Id { get; set; }
        public string MergeOrderNum { get; set; }
        [Display(Name ="入库单号")]
        public string OrderNum { get; set; }
        [Display(Name = "入库单类型")]
        public int InType { get; set; }
        public int ProductType { get; set; }
        public string StorageNum { get; set; }
        [Display(Name = "供应商编号")]
        public string SupNum { get; set; }
        [Display(Name = "供应商名称")]
        public string SupName { get; set; }
        [Display(Name = "供应商联系人")]
        public string ContactName { get; set; }
        [Display(Name = "供应商联系方式")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ContractOrder { get; set; }
        public int ContractType { get; set; }
        public int Status { get; set; }
        public string StatusStr { get; set; }
        public int IsDelete { get; set; }
        [Display(Name ="待扫描数量")]
        public double Num { get; set; }
        [Display(Name = "入库描数量")]
        public double RealNum { get; set; }
        [Display(Name = "上架数量")]
        public double PutNum { get; set; }
        public double Amount { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public DateTime? OrderTime { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string AuditUser { get; set; }
        public DateTime? AuditeTime { get; set; }
        public string PrintUser { get; set; }
        public DateTime? PrintTime { get; set; }
        public string StoreKeeper { get; set; }
        public string Reason { get; set; }
        public int OperateType { get; set; }
        public string EquipmentNum { get; set; }
        public string EquipmentCode { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        public int CompanyId { get; set; }
        public List<SelectListItem> Suppliers { get; set; }
        public string CreateTimeStr { get; set; }

        public double? Price { get; set; }
    }
}
