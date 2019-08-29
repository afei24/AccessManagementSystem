using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.DOTS.WMS.IMS;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSData;

namespace AccessManagementServices.Services
{
    public class BasicInfoServices
    {
        AccessManagementContext _context;
        private LuJCDBContext _contextWMS;
        public BasicInfoServices(AccessManagementContext context, LuJCDBContext contextWMS)
        {
            _context = context;
            _contextWMS = contextWMS;
        }
        public IList<SelectListItem> GetCompanyStatus()
        {
            return EnumHelper.EnumToList<ComapnyStatus>();
        }

        public IList<SelectListItem> GetAccountStatus()
        {
            return EnumHelper.EnumToList<AccountStatus>();
        }

        public IList<SelectListItem> GetLocalType()
        {
            return EnumHelper.EnumToList<LocalType>();
        }
        public IList<SelectListItem> GetSupType()
        {
            return EnumHelper.EnumToList<SupType>();
        }
        
        public async Task<IList<AppMenuViewModel>> GetParentAppMenu()
        {
            var parentAM = _context.AppMenu.Where(a=>a.ParentId == null || a.ParentId == 0);
            var vms = await parentAM.ProjectTo<AppMenuViewModel>().ToListAsync();
            return vms;
        }

        public async Task<List<SelectListItem>> GetRoles(AccountViewModel account)
        {
            var role = _context.Role.Where(a => a.CompanyId == account.CompanyId);
            var vms = role.ProjectTo<RoleViewModel>()
                .Select(o=> new SelectListItem() { Text = o.Name,Value = o.Id.ToString()}).ToList();
            return vms;
        }

        public async Task<List<SelectListItem>> GetBranchs(AccountViewModel account)
        {
            var branchs = _context.Branch.Where(a => a.CompanyId == account.CompanyId);
            var vms = branchs.ProjectTo<BranchViewModel>()
                .Select(o => new SelectListItem() { Text = o.Name, Value = o.Id.ToString() }).ToList();
            return vms;
        }
        public async Task<List<SelectListItem>> GetGoods(AccountViewModel account)
        {
            var branchs = _contextWMS.ProductCategory.Where(a => a.CompanyId == account.CompanyId);
            var vms = branchs.ProjectTo<ProductCategoryViewModel>()
                .Select(o => new SelectListItem() { Text = o.CateName, Value = o.CateName }).ToList();
            return vms;
        }

        public async Task<List<SelectListItem>> GetCustomers(AccountViewModel account)
        {
            var branchs = _contextWMS.Customer.Where(a => a.CompanyId == account.CompanyId);
            var vms = branchs.ProjectTo<CustomerViewModel>()
                .Select(o => new SelectListItem() { Text = o.CusName, Value = o.Id.ToString() }).ToList();
            return vms;
        }

        public async Task<List<SelectListItem>> GetMeasures(AccountViewModel account)
        {
            var branchs = _contextWMS.Measure.Where(a => a.CompanyId == account.CompanyId);
            var vms = branchs.ProjectTo<MeasureViewModel>()
                .Select(o => new SelectListItem() { Text = o.MeasureName, Value = o.MeasureName }).ToList();
            return vms;
        }
        public async Task<List<SelectListItem>> GetLocations(AccountViewModel account)
        {
            var branchs = _contextWMS.Location.Where(a => a.CompanyId == account.CompanyId);
            var vms = branchs.ProjectTo<LocationViewModel>()
                .Select(o => new SelectListItem() { Text = o.LocalBarCode, Value = o.Id.ToString() }).ToList();
            return vms;
        }
    }
}
