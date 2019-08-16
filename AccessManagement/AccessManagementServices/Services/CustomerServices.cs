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
    public class CustomerServices: BaseServices
    {
        private LuJCDBContext _context;
        public CustomerServices(LuJCDBContext context, ILogger<CustomerServices> logger)
            : base(logger)
        {
            _context = context;
        }
        public async Task<ResponseModel<CustomerViewModel>> GetList(CustomerFilters filters, SortCol sortCol)
        {
            var query = _context.Customer.Where(o => o.Id != 0);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<CustomerViewModel>().ToListAsync();
            ResponseModel<CustomerViewModel> result = new ResponseModel<CustomerViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<CustomerViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.Location.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<CustomerViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<Customer> Search(IQueryable<Customer> query, CustomerFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o => o.CusName.Contains(filters.Name));
            }
            return query;
        }
        public IQueryable<Customer> Sort(IQueryable<Customer> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                    break;
                case "localName":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.CusName) :
                        query.OrderByDescending(o => o.CusName);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }

        public async Task<ServiceResponseBase> Create(CustomerViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.Customer.AnyAsync(o => o.CusName == vm.CusName
                     && o.CompanyId == account.CompanyId);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复库位" };
                }
                //vm.CompanyId = account.CompanyId;
                var customer = Mapper.Map<Customer>(vm);
                await _context.Customer.AddAsync(customer);
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }
        }
        public async Task<ServiceResponseBase> Update(CustomerViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.Customer.AnyAsync(o => o.CusName == vm.CusName
                     && o.CompanyId == account.CompanyId && o.Id != vm.Id);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复角色" };
                }
                var customer = await _context.Customer.FirstOrDefaultAsync(o => o.Id == vm.Id);
                Mapper.Map(vm, customer);
                _context.Entry(customer).State = EntityState.Modified;
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
                    var customer = await _context.Customer.FirstOrDefaultAsync(o => o.Id == _id);
                    if (customer != null)
                    {
                        _context.Entry(customer).State = EntityState.Deleted;
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
