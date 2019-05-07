using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.Common
{
    public enum ComapnyStatus
    {
        正常= 0,
        冻结 =1,
    }
    public enum CompanyType
    {

    }

    public enum AccountType
    {
        普通用户 = 0, 企业管理员 = 1, 平台管理员 = 2, 代理用户 = 3
    }

    public enum AccountStatus
    {
        正常 = 0, 冻结 = 1, 删除 = 2
    }
}
