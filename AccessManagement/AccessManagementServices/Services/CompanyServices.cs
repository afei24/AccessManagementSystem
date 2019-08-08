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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementServices.Services
{
    public class CompanyServices
    {
        IMapper _mapper;
        AccessManagementContext _context;
        public CompanyServices(AccessManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CompanyViewModel>> GetList()
        {
            try
            {
                var query = _context.Company;
                return await _mapper.ProjectTo<CompanyViewModel>(query).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }

        public async Task<ResponseModel<CompanyViewModel>> GetList(CompanyFilters filters, SortCol sortCol, AccountViewModel current)
        {
            var query = _context.Company.Where(o => o.Id != 0);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<CompanyViewModel>().ToListAsync();
            ResponseModel<CompanyViewModel> result = new ResponseModel<CompanyViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public IQueryable<Company> Search(IQueryable<Company> query, CompanyFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o => o.Name.Contains(filters.Name));
            }
            return query;
        }
        public IQueryable<Company> Sort(IQueryable<Company> query, SortCol sortCol)
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
                case "createTime":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.CreateTime) :
                        query.OrderByDescending(o => o.CreateTime);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }


        public async Task<CompanyViewModel> GetById(int id)
        {
            try
            {
                var query =await _context.Company.FirstOrDefaultAsync(o=>o.Id==id);
                var vm =  _mapper.Map<CompanyViewModel>(query);
                var functions =  await _context.Function.Where(o => o.CompanyId == vm.Id).ToListAsync();
                vm.ComapnyStatuss = EnumHelper.EnumToList<ComapnyStatus>();
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<CompanyViewModel> GetSettingById(int id)
        {
            try
            {
                var query = await _context.Company.FirstOrDefaultAsync(o => o.Id == id);
                var vm = _mapper.Map<CompanyViewModel>(query);
                var reSetFunctions =await _context.ReSetFunction.ToListAsync();
                var functions = await _context.Function.Where(o => o.CompanyId == vm.Id).ToListAsync();
                var appMenus =await  _context.AppMenu.ToListAsync();
                foreach (var reSetFunction in reSetFunctions)
                {
                    var appMenu = appMenus.FirstOrDefault(o=>o.Code==reSetFunction.Code);
                    var function = functions.FirstOrDefault(o=>o.Code== reSetFunction.Code);
                    var res = SetOrderIncomeProperty(reSetFunction, function);
                    if (res != null)
                    {
                        vm.CompanyFunctionViewModels.Add(
                            new CompanyFunctionViewModel()
                            {
                                Name = reSetFunction.Name,
                                Code = reSetFunction.Code,
                                AppMenuName = appMenu.Name,
                                ParentAppMenuName = appMenu.Parent != null ? appMenu.Parent.Name : "",
                                FunctionSelecteds = res
                            });
                    }
                }

                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<FunctionSelected> SetOrderIncomeProperty(ReSetFunction reSetFunction, Function function)
        {
            List<FunctionSelected> functionSelecteds = new List<FunctionSelected>();
            var rf = reSetFunction.GetType();
            if (function != null)
            {
                var f = function.GetType();
                object[] rfs = rf.GetProperties();

                foreach (PropertyInfo pi in rfs)
                {
                    if (pi.Name.Contains("OpName"))
                    {
                        var propertyInfo = f.GetProperty(pi.Name);
                        if (propertyInfo != null && pi.GetValue(reSetFunction) != null 
                            && !string.IsNullOrWhiteSpace(pi.GetValue(reSetFunction).ToString()))
                        {
                            functionSelecteds.Add(new FunctionSelected()
                            {
                                Name = pi.GetValue(reSetFunction).ToString(),
                                Selected = (pi.GetValue(reSetFunction).ToString() == propertyInfo.GetValue(function).ToString())
                            });

                        }
                    }

                }
            }
            else
            {
                object[] rfs = rf.GetProperties();

                foreach (PropertyInfo pi in rfs)
                {
                    if (pi.Name.Contains("OpName"))
                    {
                        if (pi.GetValue(reSetFunction) != null && !string.IsNullOrWhiteSpace(pi.GetValue(reSetFunction).ToString()))
                        {
                            functionSelecteds.Add(new FunctionSelected()
                            {
                                Name = pi.GetValue(reSetFunction).ToString(),
                                Selected = false
                            });

                        }
                    }

                }
            }

            return functionSelecteds;
        }

        public async Task<ServiceResponseBase> Create(CompanyViewModel vm,AccountViewModel current)
        {
            try
            {
                vm.CreateTime = DateTime.Now;
                var company = _mapper.Map<Company>(vm);
                await _context.Company.AddAsync(company);
                var branch = new Branch()
                {
                    Name ="总部",
                    Company = company,
                    CreateTime = DateTime.Now,
                    CreateUserId = current.Id,
                    City = ""
                };
                await _context.Branch.AddAsync(branch);
                var account = new Account() {
                    Branch = branch,
                    Company = company,
                    AccountName = "admin"+company.Id,
                    Password = "admin123",
                    Name = "管理员",
                    Type = AccountType.普通用户,
                    Status = AccountStatus.正常,
                    CreateTime = DateTime.Now,
                    CreateUserId = current.Id,
                    City = ""
                };
                await _context.Account.AddAsync(account);
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase() { Status = Status.error,Message = ex.Message };
            }
            
        }

        public async Task<ServiceResponseBase> Update(CompanyViewModel vm)
        {
            try
            {
                var query =  await _context.Company.FirstOrDefaultAsync(o => o.Id == vm.Id);
                vm.UpdateTime = DateTime.Now;
                vm.CreateTime = query.CreateTime;
                Mapper.Map(vm,query);
                _context.Entry(query).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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
                    var company = await _context.Company.FirstOrDefaultAsync(o => o.Id == _id);
                    if (company != null)
                    {
                        company.Status = ComapnyStatus.冻结;
                        _context.Entry(company).State = EntityState.Modified;
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
