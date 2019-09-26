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
        public async Task<ResponseModel<BadReportViewModel>> GetList(BadReportFilters filters, SortCol sortCol, AccountViewModel acoount)
        {
            var query = _context.BadReport.Where(o => o.IsDelete == 0 && o.CompanyId == acoount.CompanyId);
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

        public async Task<ResponseModel<BadReportDetailViewModel>> GetListReport(BadReportFilters filters, SortCol sortCol
            , AccountViewModel acoount)
        {
            var query = _context.BadReport.Where(o => o.IsDelete == 0 && o.CompanyId == acoount.CompanyId)
                .Select(o => new BadReport()
                {
                    OrderNum = o.OrderNum,
                    Num = o.Num,
                    CreateTime = o.CreateTime.Date
                });
            query = Search(query, filters);
            var orderNums = await query.Select(o => o.OrderNum).ToListAsync();
            var details = _context.BadReportDetail.Where(o => orderNums.Contains(o.OrderNum)).ToList();
            var _query = details.GroupBy(o => o.ProductNum).Select(o => new BadReportDetailViewModel() { ProductNum = o.Key, Num = o.Count() });
            var vms =  _query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit).ToList();
            foreach (var vm in vms)
            {
                var detail = details.FirstOrDefault(o=>o.ProductNum == vm.ProductNum);
                vm.ProductName = detail.ProductName;
                vm.BarCode = detail.BarCode;
                vm.Num = details.Where(o => o.ProductNum == vm.ProductNum).Sum(o=>o.Num);
            }
            ResponseModel<BadReportDetailViewModel> result = new ResponseModel<BadReportDetailViewModel>();
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
                var query = await _context.BadReport.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<BadReportViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<List<BadReportDetailViewModel>> GetDetailByOrderNum(string orderNum)
        {
            try
            {
                var query = await _context.BadReportDetail.Where(o => o.OrderNum == orderNum)
                    .ProjectTo<BadReportDetailViewModel>().ToListAsync();
                return query;
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
            if (!string.IsNullOrWhiteSpace(filters.Status))
            {
                var status = Convert.ToInt32(filters.Status);
                query = query.Where(o => o.Status == status);
            }
            if (!string.IsNullOrWhiteSpace(filters.StartDateTime))
            {
                var startTime = Convert.ToDateTime(filters.StartDateTime);
                query = query.Where(o => o.CreateTime >= startTime);
            }
            if (!string.IsNullOrWhiteSpace(filters.EndDateTime))
            {
                var endTime = Convert.ToDateTime(filters.EndDateTime);
                query = query.Where(o => o.CreateTime < endTime);
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

        public async Task<ServiceResponseBase> Create(BadReportViewModel vm, List<BadReportDetailViewModel> details, AccountViewModel account)
        {
            try
            {
                var badReport = Mapper.Map<BadReport>(vm);
                badReport.Num = details.Sum(o=>o.Num);
                badReport.CompanyId = account.CompanyId;
                badReport.CreateTime = DateTime.Now;
                badReport.CreateUser = account.Name;
                badReport.StorageNum = account.BranchId.ToString();
                badReport.Status = (int)BadStatus.等待审核;
                await _context.BadReport.AddAsync(badReport);
                foreach (var detail in details)
                {
                    var entry = Mapper.Map<BadReportDetail>(detail);
                    entry.OrderNum = badReport.OrderNum;
                    entry.CreateTime = DateTime.Now;
                    entry.BarCode = entry.BarCode;
                    entry.SnNum = Guid.NewGuid().ToString("N");
                    await _context.BadReportDetail.AddAsync(entry);
                }
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }
        }
        public async Task<ServiceResponseBase> Update(BadReportViewModel vm, List<BadReportDetailViewModel> details, AccountViewModel account)
        {
            try
            {
                var badReport = await _context.BadReport.FirstOrDefaultAsync(o => o.Id == vm.Id);
                badReport.BadType = vm.BadType;
                badReport.Remark = vm.Remark;
                badReport.CompanyId = account.CompanyId;
                badReport.Num = details.Sum(o => o.Num);
                _context.Entry(badReport).State = EntityState.Modified;
                var _details = await _context.BadReportDetail.Where(o => o.OrderNum == badReport.OrderNum).ToListAsync();
                foreach (var _detail in _details)
                {
                    var detail = details.FirstOrDefault(o => o.ProductNum == _detail.ProductNum
                        && o.FromLocalNum == _detail.FromLocalNum);
                    if (detail == null)
                    {
                        _context.BadReportDetail.Remove(_detail);
                    }
                }

                foreach (var detail in details)
                {
                    var _detail = _details.FirstOrDefault(o => o.ProductNum == detail.ProductNum
                        && o.FromLocalNum == detail.FromLocalNum);
                    if (_detail != null)
                    {
                        _detail.Num = detail.Num;
                    }
                    else
                    {
                        var entry = Mapper.Map<BadReportDetail>(detail);
                        entry.OrderNum = badReport.OrderNum;
                        entry.CreateTime = DateTime.Now;
                        entry.BarCode = entry.BarCode;
                        entry.SnNum = Guid.NewGuid().ToString("N");
                        entry.Num = detail.Num;
                        await _context.BadReportDetail.AddAsync(entry);
                    }
                }
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

        public async Task<ServiceResponseBase> Check(int id,int badStatus, AccountViewModel account)
        {
            try
            {
                var badReport = await _context.BadReport.FirstOrDefaultAsync(o => o.Id == id);
                if (badReport != null)
                {
                    badReport.AuditeTime = DateTime.Now;
                    badReport.AuditUser = account.Name;
                    badReport.Status = badStatus;
                    _context.Entry(badReport).State = EntityState.Modified;
                    if (badStatus == (int)BadStatus.审核成功)
                    {
                        var details = await _context.BadReportDetail.Where(o => o.OrderNum == badReport.OrderNum).ToListAsync();
                        foreach (var detail in details)
                        {
                            var localProduct = await _context.LocalProduct.FirstOrDefaultAsync(o => o.ProductNum == detail.ProductNum
                                 && o.LocalNum == detail.FromLocalNum);
                            if (localProduct != null)
                            {
                                localProduct.InvalidNum = (int)detail.Num;
                            }
                        }
                        
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
