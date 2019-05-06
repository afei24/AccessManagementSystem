using AccessManagementServices.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementServices.Services
{
    public class BasicInfoServices
    {
        public IList<SelectListItem> GetCompanyStatus()
        {
            return EnumHelper.EnumToList<ComapnyStatus>();
        }
    }
}
