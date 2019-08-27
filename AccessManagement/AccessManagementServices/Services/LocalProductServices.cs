using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.DOTS.WMS.Report;
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
    public class LocalProductServices : BaseServices
    {
        private LuJCDBContext _context;
        public LocalProductServices(LuJCDBContext context, ILogger<LocalProductServices> logger)
            : base(logger)
        {
            _context = context;
        }
        public async Task<ResponseModel<LocalProductViewModel>> GetList(LocalProductFilters filters, SortCol sortCol)
        {
            var query = _context.LocalProduct.Where(o => o.Id != 0);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<LocalProductViewModel>().ToListAsync();
            ResponseModel<LocalProductViewModel> result = new ResponseModel<LocalProductViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<LocalProductViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.LocalProduct.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<LocalProductViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<LocalProduct> Search(IQueryable<LocalProduct> query, LocalProductFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.OrderNum))
            {
                query = query.Where(o => o.BarCode.Contains(filters.OrderNum));
            }
            return query;
        }
        public IQueryable<LocalProduct> Sort(IQueryable<LocalProduct> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                    break;
                case "orderNum":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.BarCode) :
                        query.OrderByDescending(o => o.BarCode);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }

        public async Task<ServiceResponseBase> Create(LocalProductViewModel vm, AccountViewModel account)
        {
            try
            {
                var localProduct = Mapper.Map<LocalProduct>(vm);
                await _context.LocalProduct.AddAsync(localProduct);
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }
        }
        public async Task<ServiceResponseBase> Update(LocalProductViewModel vm, AccountViewModel account)
        {
            try
            {
                var localProduct = await _context.LocalProduct.FirstOrDefaultAsync(o => o.Id == vm.Id);
                Mapper.Map(vm, localProduct);
                _context.Entry(localProduct).State = EntityState.Modified;
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
                    var localProduct = await _context.LocalProduct.FirstOrDefaultAsync(o => o.Id == _id);
                    if (localProduct != null)
                    {
                        _context.Entry(localProduct).State = EntityState.Deleted;
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
