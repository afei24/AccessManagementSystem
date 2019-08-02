using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.Filters;
using AccessManagementServices.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementServices.Services
{
    public class AppMenuServices
    {
        private AccessManagementContext _accessManagementContext;
        public AppMenuServices(AccessManagementContext accessManagementContext)
        {
            _accessManagementContext = accessManagementContext;
        }

        public async Task<ResponseModel<AppMenuViewModel>> GetList(AppmenuFilters filters, SortCol sortCol)
        {
            var query =  _accessManagementContext.AppMenu.Where(o=>o.Id != 0);
            query = Search(query,filters);
            query = Sort(query,sortCol);
            var vms =await query.Skip((filters.Page-1)*filters.Limit).Take(filters.Limit)
                .ProjectTo<AppMenuViewModel>().ToListAsync();
            ResponseModel<AppMenuViewModel> result = new ResponseModel<AppMenuViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public IQueryable<AppMenu> Search(IQueryable<AppMenu> query, AppmenuFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o=>o.Name.Contains(filters.Name));
            }
            if (!string.IsNullOrWhiteSpace(filters.Code))
            {
                query = query.Where(o => o.Code.Contains(filters.Code));
            }
            return query;
        }
        public IQueryable<AppMenu> Sort(IQueryable<AppMenu> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o=>o.Id);
                    break;
                case "order":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Order) :
                        query.OrderByDescending(o => o.Order);
                    break;
                case "code":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Code) :
                        query.OrderByDescending(o => o.Code);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }

        public async Task<AppMenuViewModel> GetById(int id)
        {
            try
            {
                var query = await _accessManagementContext.AppMenu.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<AppMenuViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ServiceResponseBase> Create(AppMenuViewModel vm)
        {
            try
            {
                var strategy = _accessManagementContext.Database.CreateExecutionStrategy();
                var isExist =await _accessManagementContext.AppMenu.AnyAsync(o=>o.Code==vm.Code);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复编码！" };
                }
                var appMenu = Mapper.Map<AppMenu>(vm);
                await _accessManagementContext.AppMenu.AddAsync(appMenu);
                
                var reSetFunction = new ReSetFunction() {
                    Name = appMenu.Name,
                    Code = appMenu.Code
                };
                await _accessManagementContext.ReSetFunction.AddAsync(reSetFunction);

                await _accessManagementContext.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }

        }

        public async Task<ServiceResponseBase> Update(AppMenuViewModel vm)
        {
            try
            {
                var isExist = await _accessManagementContext.AppMenu.AnyAsync(o => o.Code == vm.Code
                && o.Id != vm.Id);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复编码！" };
                }
                var query = await _accessManagementContext.AppMenu.FirstOrDefaultAsync(o => o.Id == vm.Id);
                if (query.Code != vm.Code)
                {
                    var reSetFunction =await  _accessManagementContext.ReSetFunction.FirstOrDefaultAsync(o=>o.Code==query.Code);
                    if (reSetFunction != null)
                    {
                        reSetFunction.Code = vm.Code;
                    }
                    var functions = await _accessManagementContext.Function.Where(o=>o.Code==query.Code).ToListAsync();
                    foreach (var function in functions)
                    {
                        function.Code = vm.Code;
                    }
                }
                Mapper.Map(vm, query);
                _accessManagementContext.Entry(query).State = EntityState.Modified;
                await _accessManagementContext.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
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
                    var appmenu =await _accessManagementContext.AppMenu.FirstOrDefaultAsync(o=>o.Id==_id);
                    if (appmenu != null)
                    {
                        _accessManagementContext.Entry(appmenu).State = EntityState.Deleted;
                    }
                }
                
                await _accessManagementContext.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }

        }
    }
}
