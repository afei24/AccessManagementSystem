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
    public class ProductServices : BaseServices
    {
        private LuJCDBContext _context;
        public ProductServices(LuJCDBContext context, ILogger<ProductServices> logger)
            : base(logger)
        {
            _context = context;
        }
        public async Task<ResponseModel<ProductViewModel>> GetList(ProductFilters filters, SortCol sortCol
            ,AccountViewModel account)
        {
            var query = _context.Product.Where(o => o.IsDelete == 0 && o.CompanyId == account.CompanyId);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<ProductViewModel>().ToListAsync();
            ResponseModel<ProductViewModel> result = new ResponseModel<ProductViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<ProductViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.Product.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<ProductViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<Product> Search(IQueryable<Product> query, ProductFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o => o.ProductName.Contains(filters.Name));
            }
            if (!string.IsNullOrWhiteSpace(filters.Code))
            {
                query = query.Where(o => o.BarCode.Contains(filters.Code));
            }
            return query;
        }
        public IQueryable<Product> Sort(IQueryable<Product> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                    break;
                case "productName":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.ProductName) :
                        query.OrderByDescending(o => o.ProductName);
                    break;
                case "barCode":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.BarCode) :
                        query.OrderByDescending(o => o.BarCode);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }

        public async Task<ServiceResponseBase> Create(ProductViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.Product.AnyAsync(o => o.BarCode == vm.BarCode
                     && o.CompanyId == account.CompanyId);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复条码" };
                }
                vm.CompanyId = account.CompanyId;
                vm.CreateTime = DateTime.Now;
                vm.CreateUser = account.Name;
                vm.SnNum = "";
                var product = Mapper.Map<Product>(vm);
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }
        }
        public async Task<ServiceResponseBase> Update(ProductViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.Product.AnyAsync(o => o.BarCode == vm.BarCode
                     && o.CompanyId == account.CompanyId && o.Id != vm.Id);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复条码" };
                }
                var product = await _context.Product.FirstOrDefaultAsync(o => o.Id == vm.Id);
                vm.SnNum = "";
                Mapper.Map(vm, product);
                _context.Entry(product).State = EntityState.Modified;
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
                    var product = await _context.Product.FirstOrDefaultAsync(o => o.Id == _id);
                    if (product != null)
                    {
                        _context.Entry(product).State = EntityState.Deleted;
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
