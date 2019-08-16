using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.DOTS.WMS.IMS;
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
    public class MeasureServices: BaseServices
    {
        private LuJCDBContext _context;
        public MeasureServices(LuJCDBContext context, ILogger<MeasureServices> logger)
            : base(logger)
        {
            _context = context;
        }
        public async Task<ResponseModel<MeasureViewModel>> GetList(MeasureFilters filters, SortCol sortCol)
        {
            var query = _context.Measure.Where(o => o.Id != 0);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<MeasureViewModel>().ToListAsync();
            ResponseModel<MeasureViewModel> result = new ResponseModel<MeasureViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<MeasureViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.Measure.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<MeasureViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<Measure> Search(IQueryable<Measure> query, MeasureFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o => o.MeasureName.Contains(filters.Name));
            }
            return query;
        }
        public IQueryable<Measure> Sort(IQueryable<Measure> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                    break;
                case "localName":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.MeasureName) :
                        query.OrderByDescending(o => o.MeasureName);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }

        public async Task<ServiceResponseBase> Create(MeasureViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.Measure.AnyAsync(o => o.MeasureName == vm.MeasureName
                     && o.CompanyId == account.CompanyId);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复库位" };
                }
                //vm.CompanyId = account.CompanyId;
                var measure = Mapper.Map<Measure>(vm);
                await _context.Measure.AddAsync(measure);
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }
        }
        public async Task<ServiceResponseBase> Update(MeasureViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.Measure.AnyAsync(o => o.MeasureName == vm.MeasureName
                     && o.CompanyId == account.CompanyId && o.Id != vm.Id);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复角色" };
                }
                var measure = await _context.Measure.FirstOrDefaultAsync(o => o.Id == vm.Id);
                Mapper.Map(vm, measure);
                _context.Entry(measure).State = EntityState.Modified;
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
                    var measure = await _context.Measure.FirstOrDefaultAsync(o => o.Id == _id);
                    if (measure != null)
                    {
                        _context.Entry(measure).State = EntityState.Deleted;
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
