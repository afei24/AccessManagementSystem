using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
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

namespace AccessManagementServices.Services
{
    public class RoleServices: BaseServices
    {
        private AccessManagementContext _accessManagementContext;
        public RoleServices(AccessManagementContext accessManagementContext,ILogger<RoleServices> logger)
            :base(logger)
        {
            _accessManagementContext = accessManagementContext;
        }

        public async Task<ResponseModel<RoleViewModel>> GetList(RoleFilters filters, SortCol sortCol,AccountViewModel account)
        {
            var query = _accessManagementContext.Role.Where(o => o.CompanyId == account.CompanyId);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<RoleViewModel>().ToListAsync();
            ResponseModel<RoleViewModel> result = new ResponseModel<RoleViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<RoleViewModel> GetById(int id)
        {
            try
            {
                var query = await _accessManagementContext.Role.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<RoleViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<Role> Search(IQueryable<Role> query, RoleFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o => o.Name.Contains(filters.Name));
            }
            return query;
        }
        public IQueryable<Role> Sort(IQueryable<Role> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                    break;
                case "name":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Name) :
                        query.OrderByDescending(o => o.Name);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }

        public async Task<ServiceResponseBase> Create(RoleViewModel vm,AccountViewModel account)
        {
            try
            {
                var isExist =await _accessManagementContext.Role.AnyAsync(o=>o.Name == vm.Name 
                    && o.CompanyId == account.CompanyId);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复角色" };
                }
                vm.CompanyId = account.CompanyId;
                var role = Mapper.Map<Role>(vm);
                await _accessManagementContext.Role.AddAsync(role);
                await _accessManagementContext.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok};
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                return new ServiceResponseBase() { Status = Status.error,Message = ex.Message };
            }
        }
        public async Task<ServiceResponseBase> Update(RoleViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _accessManagementContext.Role.AnyAsync(o => o.Name == vm.Name
                     && o.CompanyId == account.CompanyId && o.Id != vm.Id);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复角色" };
                }
                var role =await _accessManagementContext.Role.FirstOrDefaultAsync(o=>o.Id == vm.Id);
                Mapper.Map(vm, role);
                _accessManagementContext.Entry(role).State = EntityState.Modified;
                await _accessManagementContext.SaveChangesAsync();
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
                    var appmenu = await _accessManagementContext.Role.FirstOrDefaultAsync(o => o.Id == _id);
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

        public async Task<List<TreeDataModel>> GenerateTree(int id, AccountViewModel account)
        {
            var functions = await _accessManagementContext.Function.Where(o => o.CompanyId == account.CompanyId).ToListAsync();
            var roleFuncs = await _accessManagementContext.FunctionRole.Where(o => o.RoleId == id).ToListAsync();
            List<TreeDataModel> trees = new List<TreeDataModel>();
            try
            {
                foreach (var func in functions)
                {
                    var roleFunc = roleFuncs.FirstOrDefault(o => o.FunctionId == func.Id);
                    if (roleFunc == null)
                    {
                        #region 生成children
                        List<TreeDataModel> childrenTrees = new List<TreeDataModel>();
                        if (!string.IsNullOrWhiteSpace(func.OpName1))
                        {
                            childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName1", title = func.OpName1, spread = true });
                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName2))
                        {
                            childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName2", title = func.OpName2, spread = true });
                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName3))
                        {
                            childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName3", title = func.OpName3, spread = true });
                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName4))
                        {
                            childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName4", title = func.OpName4, spread = true });
                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName5))
                        {
                            childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName5", title = func.OpName5, spread = true });
                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName6))
                        {
                            childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName6", title = func.OpName6, spread = true });
                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName7))
                        {
                            childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName7", title = func.OpName7, spread = true });
                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName8))
                        {
                            childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName8", title = func.OpName8, spread = true });
                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName9))
                        {
                            childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName9", title = func.OpName9, spread = true });
                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName10))
                        {
                            childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName10", title = func.OpName10, spread = true });
                        }
                        #endregion
                        trees.Add(new TreeDataModel()
                        {
                            id = func.Code,
                            title = func.Name,
                            children = childrenTrees.ToArray(),
                            spread = true
                        });
                    }
                    else
                    {
                        #region 生成children
                        List<TreeDataModel> childrenTrees = new List<TreeDataModel>();
                        var roleFuncOpNames = roleFunc.OpNames.Split(",");
                        if (!string.IsNullOrWhiteSpace(func.OpName1))
                        {
                            if (roleFuncOpNames.Contains("OpName1"))
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName1", title = func.OpName1, Checked = true, spread = true });
                            }
                            else
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName1", title = func.OpName1, spread = true });
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName2))
                        {
                            if (roleFuncOpNames.Contains("OpName2"))
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName2", title = func.OpName2, Checked = true, spread = true });
                            }
                            else
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName2", title = func.OpName2, spread = true });
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName3))
                        {
                            if (roleFuncOpNames.Contains("OpName3"))
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName3", title = func.OpName3, Checked = true, spread = true });
                            }
                            else
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName3", title = func.OpName3, spread = true });
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName4))
                        {
                            if (roleFuncOpNames.Contains("OpName4"))
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName4", title = func.OpName4, Checked = true, spread = true });
                            }
                            else
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName4", title = func.OpName4, spread = true });
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName5))
                        {
                            if (roleFuncOpNames.Contains("OpName5"))
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName5", title = func.OpName5, Checked = true, spread = true });
                            }
                            else
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName5", title = func.OpName5, spread = true });
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName6))
                        {
                            if (roleFuncOpNames.Contains("OpName6"))
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName6", title = func.OpName6, Checked = true, spread = true });
                            }
                            else
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName6", title = func.OpName6, spread = true });
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName7))
                        {
                            if (roleFuncOpNames.Contains("OpName7"))
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName7", title = func.OpName7, Checked = true, spread = true });
                            }
                            else
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName7", title = func.OpName7, spread = true });
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName8))
                        {
                            if (roleFuncOpNames.Contains("OpName8"))
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName8", title = func.OpName8, Checked = true, spread = true });
                            }
                            else
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName8", title = func.OpName8, spread = true });
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName9))
                        {
                            if (roleFuncOpNames.Contains("OpName9"))
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName9", title = func.OpName9, Checked = true, spread = true });
                            }
                            else
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName9", title = func.OpName9, spread = true });
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(func.OpName10))
                        {
                            if (roleFuncOpNames.Contains("OpName10"))
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName10", title = func.OpName10, Checked = true, spread = true });
                            }
                            else
                            {
                                childrenTrees.Add(new TreeDataModel() { id = func.Code + ",OpName10", title = func.OpName10, spread = true });
                            }

                        }
                        #endregion
                        trees.Add(new TreeDataModel()
                        {
                            id = func.Code,
                            title = func.Name,
                            children = childrenTrees.ToArray(),
                            spread = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
            }
            return trees;
        }

        public async Task<ServiceResponseBase> UpdateFunctionTree(TreeData models, AccountViewModel account)
        {
            try
            {
                //var reFunctions = await GetList();
                var roleFuncs =await _accessManagementContext.FunctionRole
                    .Where(o=>o.RoleId== Convert.ToInt32(models.Id)).ToListAsync();
                foreach (var roleFunc in roleFuncs)
                {
                    var function =await _accessManagementContext.Function.FirstOrDefaultAsync(o=>o.Id== roleFunc.FunctionId);
                    var _function = models.DataTree.FirstOrDefault(o => o.id == function.Code);
                    if (_function == null)
                    {
                        _accessManagementContext.FunctionRole.Remove(roleFunc);
                    }
                    else
                    {
                        #region 更新functions
                        string opNames = "";
                        foreach (var d in _function.children)
                        {
                            if (d.id.Contains("OpName1"))
                            {
                                opNames += "OpName1,";
                            }

                            if (d.id.Contains("OpName2"))
                            {
                                opNames += "OpName2,";
                            }

                            if (d.id.Contains("OpName3"))
                            {
                                opNames += "OpName3,";
                            }

                            if (d.id.Contains("OpName4"))
                            {
                                opNames += "OpName4,";
                            }

                            if (d.id.Contains("OpName5"))
                            {
                                opNames += "OpName5,";
                            }

                            if (d.id.Contains("OpName6"))
                            {
                                opNames += "OpName6,";
                            }

                            if (d.id.Contains("OpName7"))
                            {
                                opNames += "OpName7,";
                            }

                            if (d.id.Contains("OpName8"))
                            {
                                opNames += "OpName8,";
                            }

                            if (d.id.Contains("OpName9"))
                            {
                                opNames += "OpName9,";
                            }

                            if (d.id.Contains("OpName10"))
                            {
                                opNames += "OpName10,";
                            }
                        }
                        #endregion
                        roleFunc.OpNames = opNames.Remove(opNames.Count()-1);
                    }
                }

                var functionIds = roleFuncs.Select(o => o.FunctionId).ToList();
                var functionCodes = await _accessManagementContext.Function.Where(o=>functionIds.Contains(o.Id))
                    .Select(o=>o.Code).ToListAsync();
                var _models = models.DataTree.Where(o => !functionCodes.Contains(o.id)).ToList();
                foreach (var model in _models)
                {
                    var function =await _accessManagementContext.Function.
                        Where(o=>o.Code== model.id && o.CompanyId == account.CompanyId).FirstOrDefaultAsync();
                    if (function == null)
                        continue;
                    var functionRole = new FunctionRole()
                    {
                        RoleId = Convert.ToInt32(models.Id),
                        FunctionId = function.Id
                    };

                    string opNames = "";
                    foreach (var d in model.children)
                    {
                        if (d.id.Contains("OpName1"))
                        {
                            opNames += "OpName1,";
                        }

                        if (d.id.Contains("OpName2"))
                        {
                            opNames += "OpName2,";
                        }

                        if (d.id.Contains("OpName3"))
                        {
                            opNames += "OpName3,";
                        }

                        if (d.id.Contains("OpName4"))
                        {
                            opNames += "OpName4,";
                        }

                        if (d.id.Contains("OpName5"))
                        {
                            opNames += "OpName5,";
                        }

                        if (d.id.Contains("OpName6"))
                        {
                            opNames += "OpName6,";
                        }

                        if (d.id.Contains("OpName7"))
                        {
                            opNames += "OpName7,";
                        }

                        if (d.id.Contains("OpName8"))
                        {
                            opNames += "OpName8,";
                        }

                        if (d.id.Contains("OpName9"))
                        {
                            opNames += "OpName9,";
                        }

                        if (d.id.Contains("OpName10"))
                        {
                            opNames += "OpName10,";
                        }
                    }
                    functionRole.OpNames = opNames.Remove(opNames.Count() - 1);
                    _accessManagementContext.FunctionRole.Add(functionRole);
                }
                await _accessManagementContext.SaveChangesAsync();
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
