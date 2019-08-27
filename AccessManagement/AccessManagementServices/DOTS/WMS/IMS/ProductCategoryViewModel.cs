using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.DOTS.WMS.IMS
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string CateNum { get; set; }
        public string CateName { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string Remark { get; set; }
        public int CompanyId { get; set; }
    }
}
