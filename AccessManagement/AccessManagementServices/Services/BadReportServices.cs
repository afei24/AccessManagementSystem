using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.DOTS.WMS.WWMS;
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
using WMSData;

namespace AccessManagementServices.Services
{
    public class BadReportServices : BaseServices
    {
        private LuJCDBContext _context;
        public BadReportServices(LuJCDBContext context, ILogger<BadReportServices> logger)
            : base(logger)
        {
            _context = context;
        }
        public async Task<ResponseModel<BadReportViewModel>> GetList(BadReportFilters filters, SortCol sortCol)
        {
            var query = _context.BadReport.Where(o => o.Id != 0);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<BadReportViewModel>().ToListAsync();
            ResponseModel<BadReportViewModel> result = new ResponseModel<BadReportViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<BadReportViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.OutStorage.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<BadReportViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<BadReport> Search(IQueryable<BadReport> query, BadReportFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.OrderNum))
            {
                query = query.Where(o => o.OrderNum.Contains(filters.OrderNum));
            }
            return query;
        }
        public IQueryable<BadReport> Sort(IQueryable<BadReport> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                    break;
                case "orderNum":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.OrderNum) :
                        query.OrderByDescending(o => o.OrderNum);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }

        public async Task<ServiceResponseBase> Create(BadReportViewModel vm, AccountViewModel account)
        {
            try
            {
                var badReport = Mapper.Map<BadReport>(vm);
                await _context.BadReport.AddAsync(badReport);
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }
        }
        public async Task<ServiceResponseBase> Update(BadReportViewModel vm, AccountViewModel account)
        {
            try
            {
                var badReport = await _context.BadReport.FirstOrDefaultAsync(o => o.Id == vm.Id);
                Mapper.Map(vm, badReport);
                _context.Entry(badReport).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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
                    var badReport = await _context.BadReport.FirstOrDefaultAsync(o => o.Id == _id);
                    if (badReport != null)
                    {
                        _context.Entry(badReport).State = EntityState.Deleted;
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
