using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementServices.Services
{
    public class AccountServices
    {
        IMapper _mapper;
        AccessManagementContext _context;
        public AccountServices(AccessManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AccountViewModel>> GetList()
        {
            try
            {
                var query = _context.Account.Include(a => a.Branch).Include(a => a.Company);
                var s = query.ToList();
                return await _mapper.ProjectTo<AccountViewModel>(query).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<AccountViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.Account.FirstOrDefaultAsync(o => o.Id == id);
                var vm = _mapper.Map<AccountViewModel>(query);
                //var functions = await _context.Function.Where(o => o.CompanyId == vm.).ToListAsync();
                //foreach (var function in functions)
                //{
                //    vm.Functions.Add(_mapper.Map<FunctionViewModel>(function));
                //}
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ServiceResponseBase> Create(AccountViewModel vm)
        {
            try
            {
                vm.CreateTime = DateTime.Now;
                var account = _mapper.Map<Account>(vm);
                await _context.Account.AddAsync(account);
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }

        }

        public async Task<ServiceResponseBase> Update(AccountViewModel vm)
        {
            try
            {
                var query = await _context.Account.FirstOrDefaultAsync(o => o.Id == vm.Id);
                //vm.UpdateTime = DateTime.Now;
                vm.CreateTime = query.CreateTime;
                query = _mapper.Map<Account>(vm);
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
