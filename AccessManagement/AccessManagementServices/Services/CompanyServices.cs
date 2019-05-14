using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
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

        public async Task<ServiceResponseBase> Create(CompanyViewModel vm)
        {
            try
            {
                vm.CreateTime = DateTime.Now;
                var company = _mapper.Map<Company>(vm);
                await _context.Company.AddAsync(company);
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
    }
}
