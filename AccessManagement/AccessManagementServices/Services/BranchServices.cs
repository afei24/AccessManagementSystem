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
    public class BranchServices
    {
        private AccessManagementContext _context;
        public BranchServices(AccessManagementContext context)
        {
            _context = context;
        }

        public async Task<List<BranchViewModel>> GetList()
        {
            var query = _context.Branch.Where(o => o.Id != 0);
            var vms = await query.ProjectTo<BranchViewModel>().ToListAsync();
            return vms;
        }
        public async Task<BranchViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.Branch.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<BranchViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ServiceResponseBase> Create(BranchViewModel vm)
        {
            try
            {
                var isExist = await _context.Branch.AnyAsync(o => o.Name == vm.Name);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复编码！" };
                }
                var branch = Mapper.Map<Branch>(vm);
                await _context.Branch.AddAsync(branch);

                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }

        }

        public async Task<ServiceResponseBase> Update(BranchViewModel vm)
        {
            try
            {
                var isExist = await _context.Branch.AnyAsync(o => o.Name == vm.Name
                && o.Id != vm.Id);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复编码！" };
                }
                var query = await _context.Branch.FirstOrDefaultAsync(o => o.Id == vm.Id);
                query = Mapper.Map<Branch>(vm);
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
