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

    public enum InOpStatus
    {
        待入库 = 1,
        已入库 = 2,
        已上架 = 3
    }

    public enum OutOpStatus
    {
        待下架 = 0,
        已下架 = 1,
        已出库 = 2
    }

    public enum InType
    {
        /// <summary>
        /// 购买相应的产品并且入到仓库
        /// </summary>
        采购收货入库 = 1,

        /// <summary>
        /// 将产品出售给客户然后因为某种原因退回仓库
        /// </summary>
        销售退货入库 = 2,

        /// <summary>
        /// 加工生产产品入到仓库
        /// </summary>
        生产产品入库 = 3,

        /// <summary>
        /// 内部借用某个物品使用完之后还回仓库入库
        /// </summary>
        领用退还入库 = 4,

        /// <summary>
        /// 从外部借入某个物品入库
        /// </summary>
        借货入库 = 5,

        /// <summary>
        /// 将物品借给其他人然后还回仓库
        /// </summary>
        借出还入 = 6,

        到货收货 = 7,

        退换货 = 8,

        退货 = 9,
    }

    public enum OutType
    {
        /// <summary>
        /// 从外部采购物品入库
        /// </summary>
        采购退货出库 = 1,

        /// <summary>
        /// 销售出产品从仓库出货
        /// </summary>
        销售提货出库 = 2,

        /// <summary>
        /// 需要某种材料或者物品出库
        /// </summary>
        领用出库 = 3,

        /// <summary>
        /// 从仓库借出某物品出库
        /// </summary>
        借货出库 = 4,

        /// <summary>
        /// 之前借入某个物品还出出库
        /// </summary>
        借入还出 = 5,

        发货出库 = 6,

        派送出库 = 7,

        提货出库 = 8,
    }

    public enum BadType
    {
        其他报损 = 0,

        损坏报损 = 1,

        丢失报损 = 2,
    }
    public enum BadStatus
    {
        等待审核 = 1,

        审核成功 = 2,

        审核失败 = 3
    }
}
