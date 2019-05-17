using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementServices.Services
{
    public class BasicInfoServices
    {
        AccessManagementContext _context;
        public BasicInfoServices(AccessManagementContext context)
        {
            _context = context;
        }
        public IList<SelectListItem> GetCompanyStatus()
        {
            return EnumHelper.EnumToList<ComapnyStatus>();
        }

        public async Task<IList<AppMenuViewModel>> GetParentAppMenu()
        {
            var parentAM = _context.AppMenu.Where(a=>a.ParentId == null || a.ParentId == 0);
            var vms = await parentAM.ProjectTo<AppMenuViewModel>().ToListAsync();
            return vms;
        }
    }
}
