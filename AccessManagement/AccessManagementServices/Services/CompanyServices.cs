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
                foreach (var function in functions)
                {
                    vm.Functions.Add(_mapper.Map<FunctionViewModel>(function));
                }
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

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
                query = _mapper.Map<Company>(vm);
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
