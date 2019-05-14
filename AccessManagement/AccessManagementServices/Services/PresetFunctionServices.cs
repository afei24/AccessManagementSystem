using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementServices.Services
{
    public class PresetFunctionServices: BaseServices
    {
        private AccessManagementContext _context;
        public PresetFunctionServices(AccessManagementContext context, ILogger<PresetFunctionServices> logger)
            :base(logger)
        {
            _context = context;
        }

        public async Task<List<PresetFunctionViewModel>> GetList()
        {
            var query = _context.ReSetFunction.Where(o => o.Id != 0);
            var vms = await query.ProjectTo<PresetFunctionViewModel>().ToListAsync();
            return vms;
        }
        public async Task<PresetFunctionViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.ReSetFunction.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<PresetFunctionViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                return null;
            }

        }


        public async Task<ServiceResponseBase> Update(PresetFunctionViewModel vm)
        {
            try
            {
                var isExist = await _context.ReSetFunction.AnyAsync(o => o.Name == vm.Name
                && o.Id != vm.Id);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复编码！" };
                }
                var query = await _context.ReSetFunction.FirstOrDefaultAsync(o => o.Id == vm.Id);
                Mapper.Map(vm, query); //不能使用Mapper.Map<ReSetFunction>(vm),会创建一个新的实例
                _context.Entry(query).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }

        }
    }
}
