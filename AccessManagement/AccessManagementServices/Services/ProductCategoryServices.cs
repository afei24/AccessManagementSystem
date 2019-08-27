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
    public class ProductCategoryServices : BaseServices
    {
        private LuJCDBContext _context;
        public ProductCategoryServices(LuJCDBContext context, ILogger<ProductCategoryServices> logger)
            : base(logger)
        {
            _context = context;
        }
        public async Task<ResponseModel<ProductCategoryViewModel>> GetList(ProductCategoryFilters filters, SortCol sortCol)
        {
            var query = _context.ProductCategory.Where(o => o.Id != 0);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<ProductCategoryViewModel>().ToListAsync();
            ResponseModel<ProductCategoryViewModel> result = new ResponseModel<ProductCategoryViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<ProductCategoryViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.ProductCategory.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<ProductCategoryViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<ProductCategory> Search(IQueryable<ProductCategory> query, ProductCategoryFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o => o.CateName.Contains(filters.Name));
            }
            return query;
        }
        public IQueryable<ProductCategory> Sort(IQueryable<ProductCategory> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                    break;
                case "cateName":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.CateName) :
                        query.OrderByDescending(o => o.CateName);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }

        public async Task<ServiceResponseBase> Create(ProductCategoryViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.ProductCategory.AnyAsync(o => o.CateName == vm.CateName
                     && o.CompanyId == account.CompanyId);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复库位" };
                }
                //vm.CompanyId = account.CompanyId;
                var productCategory = Mapper.Map<ProductCategory>(vm);
                await _context.ProductCategory.AddAsync(productCategory);
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }
        }
        public async Task<ServiceResponseBase> Update(ProductCategoryViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.ProductCategory.AnyAsync(o => o.CateName == vm.CateName
                     && o.CompanyId == account.CompanyId && o.Id != vm.Id);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复角色" };
                }
                var productCategory = await _context.ProductCategory.FirstOrDefaultAsync(o => o.Id == vm.Id);
                Mapper.Map(vm, productCategory);
                _context.Entry(productCategory).State = EntityState.Modified;
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
                    var productCategory = await _context.ProductCategory.FirstOrDefaultAsync(o => o.Id == _id);
                    if (productCategory != null)
                    {
                        _context.Entry(productCategory).State = EntityState.Deleted;
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
