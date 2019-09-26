using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.WWMS
{
    [Serializable]
    public class InStorDetailViewModel
    {
        public int Id { get; set; }
        public string SnNum { get; set; }
        public string OrderNum { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string ProductNum { get; set; }
        public string BatchNum { get; set; }
        public double Num { get; set; }
        public int IsPick { get; set; }
        public double RealNum { get; set; }
        public double InPrice { get; set; }
        public double Amount { get; set; }
        public string ContractOrder { get; set; }
        public DateTime CreateTime { get; set; }
        public string LocalNum { get; set; }
        public string StorageNum { get; set; }
        public int Status { get; set; }
        public string OpUser { get; set; }
        public DateTime? OpTime { get; set; }
        public string ProductModel { get; set; }
        public string ProductSpecification { get; set; }
        public DateTime? ProductEffect { get; set; }
        public string Consignee { get; set; }
        public string ReceivesException { get; set; }
        public string ExceptionHandling { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public double? PutRealNum { get; set; }
    }
}
