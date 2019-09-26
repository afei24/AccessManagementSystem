using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.DOTS.WMS.IMS;
using AccessManagementServices.Filters;
using AccessManagementServices.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSData;

namespace AccessManagementServices.Services
{
    public class SupplierServices:BaseServices
    {
        private LuJCDBContext _context;
        public SupplierServices(LuJCDBContext context, ILogger<SupplierServices> logger)
            : base(logger)
        {
            _context = context;
        }
        public async Task<ResponseModel<SupplierViewModel>> GetList(SupplierFilter filters, SortCol sortCol, AccountViewModel account)
        {
            var query = _context.Supplier.Where(o => o.IsDelete == 0 && o.CompanyId == account.CompanyId);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<SupplierViewModel>().ToListAsync();
            ResponseModel<SupplierViewModel> result = new ResponseModel<SupplierViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<SupplierViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.Supplier.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<SupplierViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<Supplier> Search(IQueryable<Supplier> query, SupplierFilter filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o => o.SupName.Contains(filters.Name));
            }
            return query;
        }
        public IQueryable<Supplier> Sort(IQueryable<Supplier> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                    break;
                case "supName":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.SupName) :
                        query.OrderByDescending(o => o.SupName);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }

        public async Task<ServiceResponseBase> Create(SupplierViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.Supplier.AnyAsync(o => o.SupName == vm.SupName
                     && o.CompanyId == account.CompanyId);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复供应商" };
                }
                vm.CompanyId = account.CompanyId;
                vm.CreateTime = DateTime.Now;
                vm.CreateUser = account.Name;
                
                var supplier = Mapper.Map<Supplier>(vm);
                supplier.SupNum = supplier.Id.ToString();
                await _context.Supplier.AddAsync(supplier);
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }
        }
        public async Task<ServiceResponseBase> Update(SupplierViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.Supplier.AnyAsync(o => o.SupName == vm.SupName
                     && o.CompanyId == account.CompanyId && o.Id != vm.Id);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复供应商" };
                }
                var supplier = await _context.Supplier.FirstOrDefaultAsync(o => o.Id == vm.Id);
                Mapper.Map(vm, supplier);
                _context.Entry(supplier).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }
        }

        public async Task<ServiceResponseBase> Delete(string idStr)
        {
            try
            {
                var ids = idStr.Split(',');
                foreach (var id in ids)
                {
                    if (string.IsNullOrWhiteSpace(id))
                        continue;

                    var _id = Convert.ToInt32(id);
                    var supplier = await _context.Supplier.FirstOrDefaultAsync(o => o.Id == _id);
                    if (supplier != null)
                    {
                        supplier.IsDelete = 1;
                    }
                }

                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }

        }
    }
}
