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

        public async Task<ResponseModel<BranchViewModel>> GetList(BranchFilters filters, SortCol sortCol, AccountViewModel current)
        {
            var query = _context.Branch.Where(o => o.Id != 0 && o.CompanyId == current.CompanyId);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<BranchViewModel>().ToListAsync();
            ResponseModel<BranchViewModel> result = new ResponseModel<BranchViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public IQueryable<Branch> Search(IQueryable<Branch> query, BranchFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o => o.Name.Contains(filters.Name));
            }
            return query;
        }
        public IQueryable<Branch> Sort(IQueryable<Branch> query, SortCol sortCol)
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

        public async Task<ServiceResponseBase> Create(BranchViewModel vm, AccountViewModel current)
        {
            try
            {
                var isExist = await _context.Branch.AnyAsync(o => o.Name == vm.Name);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复编码！" };
                }
                var branch = Mapper.Map<Branch>(vm);
                branch.CreateUserId = current.Id;
                branch.CreateTime = DateTime.Now;
                branch.CompanyId = current.CompanyId;
                await _context.Branch.AddAsync(branch);

                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }

        }

        public async Task<ServiceResponseBase> Update(BranchViewModel vm, AccountViewModel current)
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
                Mapper.Map(vm, query);
                query.CreateUserId = current.Id;
                query.CreateTime = DateTime.Now;
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
                    var branch = await _context.Branch.FirstOrDefaultAsync(o => o.Id == _id);
                    _context.Entry(branch).State = EntityState.Deleted;
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
