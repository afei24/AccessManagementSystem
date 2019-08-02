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
        public async Task<List<FunctionViewModel>> GetFunctionList(int companyId)
        {
            var query = _context.Function.Where(o => o.Id != 0 & o.CompanyId== companyId);
            var vms = await query.ProjectTo<FunctionViewModel>().ToListAsync();
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

        public async Task<ServiceResponseBase> UpdateFunctionTree(TreeData models)
        {
            try
            {
                //var reFunctions = await GetList();
                var functions =await _context.Function.
                    Where(o => o.Id != 0 & o.CompanyId == Convert.ToInt32(models.Id)).ToListAsync();
                foreach (var function in functions)
                {
                    var _function =  models.DataTree.FirstOrDefault(o=>o.id == function.Code);
                    if (_function == null)
                    {
                        _context.Function.Remove(function);
                    }
                    else
                    {
                        #region 更新functions
                        var opNames = _function.children.Select(o=>o.id).ToList();
                        foreach (var d in _function.children)
                        {
                            if (d.id.Contains("OpName1"))
                            {
                                function.OpName1 = d.title;
                            }
                            else
                            {
                                if(!opNames.Contains(_function.id+","+ "OpName1"))
                                    function.OpName1 = null;
                            }
                            if (d.id.Contains("OpName2"))
                            {
                                function.OpName2 = d.title;
                            }
                            else
                            {
                                if (!opNames.Contains(_function.id + "," + "OpName1"))
                                    function.OpName2 = null;
                            }
                            if (d.id.Contains("OpName3"))
                            {
                                function.OpName3 = d.title;
                            }
                            else
                            {
                                if (!opNames.Contains(_function.id + "," + "OpName1"))
                                    function.OpName3 = null;
                            }
                            if (d.id.Contains("OpName4"))
                            {
                                function.OpName4 = d.title;
                            }
                            else
                            {
                                if (!opNames.Contains(_function.id + "," + "OpName1"))
                                    function.OpName4 = null;
                            }
                            if (d.id.Contains("OpName5"))
                            {
                                function.OpName5 = d.title;
                            }
                            else
                            {
                                if (!opNames.Contains(_function.id + "," + "OpName1"))
                                    function.OpName5 = null;
                            }
                            if (d.id.Contains("OpName6"))
                            {
                                function.OpName6 = d.title;
                            }
                            else
                            {
                                if (!opNames.Contains(_function.id + "," + "OpName1"))
                                    function.OpName6 = null;
                            }
                            if (d.id.Contains("OpName7"))
                            {
                                function.OpName7 = d.title;
                            }
                            else
                            {
                                if (!opNames.Contains(_function.id + "," + "OpName1"))
                                    function.OpName7 = null;
                            }
                            if (d.id.Contains("OpName8"))
                            {
                                function.OpName8 = d.title;
                            }
                            else
                            {
                                if (!opNames.Contains(_function.id + "," + "OpName1"))
                                    function.OpName8 = null;
                            }
                            if (d.id.Contains("OpName9"))
                            {
                                function.OpName9 = d.title;
                            }
                            else
                            {
                                if (!opNames.Contains(_function.id + "," + "OpName1"))
                                    function.OpName9 = null;
                            }
                            if (d.id.Contains("OpName10"))
                            {
                                function.OpName10 = d.title;
                            }
                            else
                            {
                                if (!opNames.Contains(_function.id + "," + "OpName1"))
                                    function.OpName10 = null;
                            }
                        }
                        #endregion
                    }
                }

                var functionCodes = functions.Select(o=>o.Code).ToList();
                var _models = models.DataTree.Where(o=>!functionCodes.Contains(o.id)).ToList();
                foreach (var model in _models)
                {
                    var function = new Function() {
                        Name = model.title,
                        Code = model.id,
                        CompanyId = Convert.ToInt32(models.Id)
                    };

                    var opNames = model.children.Select(o => o.id).ToList();
                    foreach (var d in model.children)
                    {
                        if (d.id.Contains("OpName1"))
                        {
                            function.OpName1 = d.title;
                        }
                        else
                        {
                            if (!opNames.Contains(model.id + "," + "OpName1"))
                                function.OpName1 = null;
                        }
                        if (d.id.Contains("OpName2"))
                        {
                            function.OpName2 = d.title;
                        }
                        else
                        {
                            if (!opNames.Contains(model.id + "," + "OpName1"))
                                function.OpName2 = null;
                        }
                        if (d.id.Contains("OpName3"))
                        {
                            function.OpName3 = d.title;
                        }
                        else
                        {
                            if (!opNames.Contains(model.id + "," + "OpName1"))
                                function.OpName3 = null;
                        }
                        if (d.id.Contains("OpName4"))
                        {
                            function.OpName4 = d.title;
                        }
                        else
                        {
                            if (!opNames.Contains(model.id + "," + "OpName1"))
                                function.OpName4 = null;
                        }
                        if (d.id.Contains("OpName5"))
                        {
                            function.OpName5 = d.title;
                        }
                        else
                        {
                            if (!opNames.Contains(model.id + "," + "OpName1"))
                                function.OpName5 = null;
                        }
                        if (d.id.Contains("OpName6"))
                        {
                            function.OpName6 = d.title;
                        }
                        else
                        {
                            if (!opNames.Contains(model.id + "," + "OpName1"))
                                function.OpName6 = null;
                        }
                        if (d.id.Contains("OpName7"))
                        {
                            function.OpName7 = d.title;
                        }
                        else
                        {
                            if (!opNames.Contains(model.id + "," + "OpName1"))
                                function.OpName7 = null;
                        }
                        if (d.id.Contains("OpName8"))
                        {
                            function.OpName8 = d.title;
                        }
                        else
                        {
                            if (!opNames.Contains(model.id + "," + "OpName1"))
                                function.OpName8 = null;
                        }
                        if (d.id.Contains("OpName9"))
                        {
                            function.OpName9 = d.title;
                        }
                        else
                        {
                            if (!opNames.Contains(model.id + "," + "OpName1"))
                                function.OpName9 = null;
                        }
                        if (d.id.Contains("OpName10"))
                        {
                            function.OpName10 = d.title;
                        }
                        else
                        {
                            if (!opNames.Contains(model.id + "," + "OpName1"))
                                function.OpName10 = null;
                        }
                    }
                    _context.Function.Add(function);
                }
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
