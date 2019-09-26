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
    public class OutStorageServices : BaseServices
    {
        private LuJCDBContext _context;
        public OutStorageServices(LuJCDBContext context, ILogger<OutStorageServices> logger)
            : base(logger)
        {
            _context = context;
        }
        public async Task<ResponseModel<OutStorageViewModel>> GetList(OutStorageFilters filters, SortCol sortCol, AccountViewModel acoount)
        {
            var query = _context.OutStorage.Where(o => o.CompanyId == acoount.CompanyId && o.IsDelete == 0);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<OutStorageViewModel>().ToListAsync();
            foreach (var vm in vms)
            {
                vm.RealNum = _context.OutStoDetail.Where(o => o.OrderNum == vm.OrderNum).Sum(o => o.RealNum);
                vm.PutNum = (double)_context.OutStoDetail.Where(o => o.OrderNum == vm.OrderNum).Sum(o => o.PutRealNum);
            }
            ResponseModel<OutStorageViewModel> result = new ResponseModel<OutStorageViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }

        public async Task<ResponseModel<OutStorageViewModel>> GetListCustomerReport(OutStorageFilters filters, SortCol sortCol
            , AccountViewModel acoount)
        {
            var query = _context.OutStorage.Where(o => o.IsDelete == 0 && o.CompanyId == acoount.CompanyId);
            query = Search(query, filters);
            var _query = query.GroupBy(o => o.CusName).Select(o => new OutStorageViewModel() { CusName = o.Key, Num = o.Count() });
            var vms = await _query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit).ToListAsync();
            ResponseModel<OutStorageViewModel> result = new ResponseModel<OutStorageViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }

        public async Task<ResponseModel<OutStorageViewModel>> GetListReport(OutStorageFilters filters, SortCol sortCol
            , AccountViewModel acoount)
        {
            var query = _context.OutStorage.Where(o => o.IsDelete == 0 && o.CompanyId == acoount.CompanyId)
                .Select(o => new OutStorage()
                {
                    OrderNum = o.OrderNum,
                    Num = o.Num,
                    CreateTime = o.CreateTime.Date
                });
            query = Search(query, filters);
            var orderNums = await query.Select(o => o.OrderNum).ToListAsync();
            var _query = query.GroupBy(o => o.CreateTime).Select(o => new OutStorageViewModel() { CreateTime = o.Key, Num = o.Count() });
            var vms = await _query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit).ToListAsync();
            foreach (var vm in vms)
            {
                var details = _context.OutStoDetail.Where(o => orderNums.Contains(o.OrderNum)
                && o.CreateTime > vm.CreateTime && o.CreateTime < vm.CreateTime.AddDays(1)).ToList();
                vm.CreateTimeStr = vm.CreateTime.ToString("yyyy-MM-dd");
                vm.Num = details.Sum(o => (double)o.PutRealNum);
                vm.Price = details.Sum(o => o.OutPrice);
            }
            ResponseModel<OutStorageViewModel> result = new ResponseModel<OutStorageViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<OutStorageViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.OutStorage.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<OutStorageViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<List<OutStoDetailViewModel>> GetDetailByOrderNum(string orderNum)
        {
            try
            {
                var query = await _context.OutStoDetail.Where(o => o.OrderNum == orderNum)
                    .ProjectTo<OutStoDetailViewModel>().ToListAsync();
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<OutStorage> Search(IQueryable<OutStorage> query, OutStorageFilters filters)
        {
            if(!string.IsNullOrWhiteSpace(filters.OrderNum))
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
        public IQueryable<OutStorage> Sort(IQueryable<OutStorage> query, SortCol sortCol)
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

        public async Task<ServiceResponseBase> Create(OutStorageViewModel vm, List<OutStoDetailViewModel> outStorDetails, AccountViewModel account)
        {
            try
            {
                var outStorage = Mapper.Map<OutStorage>(vm);
                outStorage.Status = (int)OutOpStatus.待下架;
                outStorage.Num = outStorDetails.Sum(o => o.Num);
                outStorage.CreateTime = DateTime.Now;
                outStorage.CreateUser = account.Name;
                outStorage.CompanyId = account.CompanyId;
                outStorage.StorageNum = account.BranchId.ToString();
                var cusId = Convert.ToInt32(vm.CusNum);
                var cus = _context.Customer.FirstOrDefault(o=>o.Id == cusId);
                outStorage.CusName = cus.CusName;
                await _context.OutStorage.AddAsync(outStorage);
                foreach (var outStorDetail in outStorDetails)
                {
                    var entry = Mapper.Map<OutStoDetail>(outStorDetail);
                    entry.OrderNum = outStorage.OrderNum;
                    entry.CreateTime = DateTime.Now;
                    entry.BarCode = entry.BarCode;
                    entry.SnNum = Guid.NewGuid().ToString("N");
                    await _context.OutStoDetail.AddAsync(entry);
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
        public async Task<ServiceResponseBase> Update(OutStorageViewModel vm, List<OutStoDetailViewModel> outStorDetails, AccountViewModel account)
        {
            try
            {
                var outStorage = await _context.OutStorage.FirstOrDefaultAsync(o => o.Id == vm.Id);

                outStorage.OutType = vm.OutType;
                outStorage.CusNum = vm.CusNum;
                outStorage.CusName = vm.CusName;
                outStorage.Contact = vm.Contact;
                outStorage.Phone = vm.Phone;
                outStorage.Remark = vm.Remark;
                var cusId = Convert.ToInt32(vm.CusNum);
                var cus = _context.Customer.FirstOrDefault(o => o.Id == cusId);
                outStorage.CusName = cus.CusName;
                var status = (OutOpStatus)outStorage.Status;
                if (status == OutOpStatus.待下架)
                {
                    outStorage.Num = outStorDetails.Sum(o => o.Num);
                }
                _context.Entry(outStorage).State = EntityState.Modified;
                var _outStorDetails = await _context.OutStoDetail.Where(o => o.OrderNum == outStorage.OrderNum).ToListAsync();
                foreach (var _outStorDetail in _outStorDetails)
                {
                    var outStorDetail = outStorDetails.FirstOrDefault(o => o.ProductNum == _outStorDetail.ProductNum
                        && o.LocalNum == _outStorDetail.LocalNum);
                    if (outStorDetail == null)
                    {
                        _context.OutStoDetail.Remove(_outStorDetail);
                    }
                }

                foreach (var outStorDetail in outStorDetails)
                {
                    var _outStorDetail = _outStorDetails.FirstOrDefault(o => o.ProductNum == outStorDetail.ProductNum
                        && o.LocalNum == outStorDetail.LocalNum);
                    if (_outStorDetail != null)
                    {
                        if (status == OutOpStatus.待下架)
                        {
                            _outStorDetail.Num = outStorDetail.Num;
                        }
                        if (status == OutOpStatus.已下架)
                        {
                            _outStorDetail.RealNum = outStorDetail.Num;
                        }
                        if (status == OutOpStatus.已出库)
                        {
                            _outStorDetail.PutRealNum = outStorDetail.Num;
                        }
                        _outStorDetail.OutPrice = outStorDetail.OutPrice;
                    }
                    else
                    {
                        var entry = Mapper.Map<OutStoDetail>(outStorDetail);
                        entry.OrderNum = outStorage.OrderNum;
                        entry.CreateTime = DateTime.Now;
                        entry.BarCode = entry.BarCode;
                        entry.SnNum = Guid.NewGuid().ToString("N");
                        if (status == OutOpStatus.待下架)
                        {
                            entry.Num = outStorDetail.Num;
                        }
                        if (status == OutOpStatus.已下架)
                        {
                            entry.RealNum = outStorDetail.Num;
                        }
                        if (status == OutOpStatus.已出库)
                        {
                            entry.PutRealNum = outStorDetail.Num;
                        }
                        await _context.OutStoDetail.AddAsync(entry);
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
                    var outStorage = await _context.OutStorage.FirstOrDefaultAsync(o => o.Id == _id);
                    if (outStorage != null)
                    {
                        outStorage.IsDelete = 1;
                        _context.Entry(outStorage).State = EntityState.Modified;
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

        public async Task<ServiceResponseBase> Check(int id, AccountViewModel account)
        {
            try
            {
                var outStorage = await _context.OutStorage.FirstOrDefaultAsync(o => o.Id == id);
                if (outStorage != null)
                {
                    outStorage.Status += 1;
                    var outStorageDetails = await _context.OutStoDetail.Where(o => o.OrderNum == outStorage.OrderNum).ToListAsync();
                    if (outStorage.Status == (int)OutOpStatus.已下架)
                    {
                        foreach (var outStorageDetail in outStorageDetails)
                        {
                            outStorageDetail.RealNum = outStorageDetail.Num;
                        }

                    }
                    if (outStorage.Status == (int)OutOpStatus.已出库)
                    {
                        foreach (var outStorageDetail in outStorageDetails)
                        {
                            outStorageDetail.PutRealNum = outStorageDetail.RealNum;
                            var localProduct = await _context.LocalProduct.FirstOrDefaultAsync(o => o.CompanyId == outStorage.CompanyId
                                && o.BarCode == outStorageDetail.BarCode && o.LocalNum == outStorageDetail.LocalNum);
                            if (localProduct == null)
                            {
                                return new ServiceResponseBase() { Status = Status.error, Message = outStorageDetail.LocalNum+"上的"+ outStorageDetail .ProductName
                                    + "库存数量不够" };

                            }
                            else
                            {
                                localProduct.Num -= (double)outStorageDetail.PutRealNum;
                                if (localProduct.Num < 0)
                                {
                                    return new ServiceResponseBase()
                                    {
                                        Status = Status.error,
                                        Message = outStorageDetail.LocalNum + "上的" + outStorageDetail.ProductName
                                    + "库存数量不够"
                                    };
                                }
                            }
                        }
                    }
                    _context.Entry(outStorage).State = EntityState.Modified;
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
