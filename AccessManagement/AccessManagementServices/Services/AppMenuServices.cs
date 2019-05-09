using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
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

        public async Task<List<AppMenuViewModel>> GetList()
        {
            var query =  _accessManagementContext.AppMenu.Where(o=>o.Id != 0);
            var vms =await query.ProjectTo<AppMenuViewModel>().ToListAsync();
            return vms;
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
                query = Mapper.Map<AppMenu>(vm);
                _accessManagementContext.Entry(query).State = EntityState.Modified;
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
