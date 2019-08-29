using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.Common
{
    public enum FunctionCode 
    {
        AccountManage = 0,
        BranchManage = 1,
        CompanyManagement = 2,
        FunctionManage = 3,
        RoleManage = 4,
        AppMenuManage = 5,
    }
    public enum CusType
    {
        /// <summary>
        /// 合作客户
        /// </summary>
        Cooperation = 1,

        /// <summary>
        /// 潜在客户
        /// </summary>
        Potential = 2,

        /// <summary>
        /// 丢失客户
        /// </summary>
        Lost = 3,

        /// <summary>
        /// 虚拟客户
        /// </summary>
        Invented = 4
    }
    public enum LocalType
    {
        /// <summary>
        /// 正式库区
        /// </summary>
        正式库区 = 1,

        /// <summary>
        /// 待入库区
        /// </summary>
        待入库区 = 2,

        /// <summary>
        /// 待检库区
        /// </summary>
        待检库区 = 3,

        /// <summary>
        /// 待出库区
        /// </summary>
        待出库区 = 4,

        /// <summary>
        /// 报损库区
        /// </summary>
        报损库区 = 5,
    }
    public enum SupType
    {
        虚拟供应商 = 1,
        合作供应商 = 2
    }
}
