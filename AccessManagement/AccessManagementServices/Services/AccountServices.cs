using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.Filters;
using AccessManagementServices.Helper;
using AccessManagementServices.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementServices.Services
{
    public class AccountServices:IAccountServices
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

        public async Task<ResponseModel<AccountViewModel>> GetList(AccountFilters filters, SortCol sortCol, AccountViewModel current)
        {
            var query = _context.Account.Where(o => o.Id != 0 && o.CompanyId == current.CompanyId);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<AccountViewModel>().ToListAsync();
            ResponseModel<AccountViewModel> result = new ResponseModel<AccountViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public IQueryable<Account> Search(IQueryable<Account> query, AccountFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o => o.Name.Contains(filters.Name));
            }
            return query;
        }
        public IQueryable<Account> Sort(IQueryable<Account> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                    break;
                case "accountName":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.AccountName) :
                        query.OrderByDescending(o => o.AccountName);
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

        public async Task<AccountViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.Account.FirstOrDefaultAsync(o => o.Id == id);
                var vm = _mapper.Map<AccountViewModel>(query);
                var accountRole = await _context.AccountRole.FirstOrDefaultAsync(o=>o.AccountId == vm.Id);
                vm.RoleId = accountRole.RoleId.ToString();
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<Account> Login(AccountViewModel vm)
        {
            try
            {
                var account = await _context.Account.Include(a => a.Branch).Include(a => a.Company)
                    .Include(a=>a.AccountFunction).Include(a => a.AccountRole)
                    .FirstOrDefaultAsync(o => o.AccountName == vm.AccountName
                    && o.Password == vm.Password);
                return account;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ServiceResponseBase> Create(AccountViewModel vm,AccountViewModel current)
        {
            try
            {
                vm.CreateTime = DateTime.Now;
                var account = _mapper.Map<Account>(vm);
                account.CompanyId = current.CompanyId;
                var role =await _context.Role.FirstOrDefaultAsync(o=>o.Id == Convert.ToInt32(vm.RoleId));
                if (role == null)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "不存在选择的角色" };
                }
                
                await _context.Account.AddAsync(account);
                await _context.AccountRole.AddAsync(new AccountRole()
                {
                    Account = account,
                    Role = role
                });

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
                Mapper.Map(vm, query);
                _context.Entry(query).State = EntityState.Modified;
                var accountRole =await  _context.AccountRole.FirstOrDefaultAsync(o=>o.AccountId == query.Id);
                if (accountRole != null && accountRole.RoleId.ToString() != vm.RoleId)
                {
                    accountRole.RoleId = Convert.ToInt32(vm.RoleId);
                    _context.Entry(accountRole).State = EntityState.Modified;
                }
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
                    var account = await _context.Account.FirstOrDefaultAsync(o => o.Id == _id);
                    if (account != null)
                    {
                        var accountRoles = await _context.AccountRole.Where(o=>o.AccountId == account.Id).ToListAsync();
                        _context.AccountRole.RemoveRange(accountRoles);
                        _context.Entry(account).State = EntityState.Deleted;
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
