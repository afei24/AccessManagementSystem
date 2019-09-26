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
    public class InStorageServices : BaseServices
    {
        private LuJCDBContext _context;
        public InStorageServices(LuJCDBContext context, ILogger<InStorageServices> logger)
            : base(logger)
        {
            _context = context;
        }
        public async Task<ResponseModel<InStorageViewModel>> GetList(InStorageFilters filters, SortCol sortCol
            ,AccountViewModel acoount)
        {
            var query = _context.InStorage.Where(o => o.IsDelete == 0 && o.CompanyId == acoount.CompanyId);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<InStorageViewModel>().ToListAsync();
            foreach (var vm in vms)
            {
                vm.RealNum = _context.InStorDetail.Where(o=>o.OrderNum == vm.OrderNum).Sum(o=>o.RealNum);
                vm.PutNum = (double)_context.InStorDetail.Where(o => o.OrderNum == vm.OrderNum).Sum(o => o.PutRealNum);
            }
            ResponseModel<InStorageViewModel> result = new ResponseModel<InStorageViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<ResponseModel<InStorageViewModel>> GetListSupReport(InStorageFilters filters, SortCol sortCol
            , AccountViewModel acoount)
        {
            var query = _context.InStorage.Where(o => o.IsDelete == 0 && o.CompanyId == acoount.CompanyId);
            query = Search(query, filters);
            var _query = query.GroupBy(o => o.SupName).Select(o => new InStorageViewModel() { SupName = o.Key, Num = o.Count() });
            var vms = await _query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit).ToListAsync();
            ResponseModel<InStorageViewModel> result = new ResponseModel<InStorageViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }

        public async Task<ResponseModel<InStorageViewModel>> GetListReport(InStorageFilters filters, SortCol sortCol
            , AccountViewModel acoount)
        {
            var query = _context.InStorage.Where(o => o.IsDelete == 0 && o.CompanyId == acoount.CompanyId)
                .Select(o=>new InStorage() {
                    OrderNum = o.OrderNum,
                    Num = o.Num,
                    CreateTime = o.CreateTime.Date
                });
            query = Search(query, filters);
            var orderNums =await query.Select(o=>o.OrderNum).ToListAsync();
            var _query = query.GroupBy(o=>o.CreateTime).Select(o=>new InStorageViewModel() { CreateTime = o.Key,Num = o.Count()});
            var vms = await _query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit).ToListAsync();
            foreach (var vm in vms)
            {
                var details = _context.InStorDetail.Where(o => orderNums.Contains(o.OrderNum) 
                && o.CreateTime>vm.CreateTime && o.CreateTime<vm.CreateTime.AddDays(1)).ToList();
                vm.CreateTimeStr = vm.CreateTime.ToString("yyyy-MM-dd");
                vm.Num = details.Sum(o => (double)o.PutRealNum);
                vm.Price = details.Sum(o =>o.InPrice);
            }
            ResponseModel<InStorageViewModel> result = new ResponseModel<InStorageViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }

        public async Task<InStorageViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.InStorage.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<InStorageViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        public async Task<List<InStorDetailViewModel>> GetDetailByOrderNum(string orderNum)
        {
            try
            {
                var query = await _context.InStorDetail.Where(o => o.OrderNum == orderNum)
                    .ProjectTo<InStorDetailViewModel>().ToListAsync();
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<InStorage> Search(IQueryable<InStorage> query, InStorageFilters filters)
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
        public IQueryable<InStorage> Sort(IQueryable<InStorage> query, SortCol sortCol)
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

        public async Task<ServiceResponseBase> Create(InStorageViewModel vm, List<InStorDetailViewModel> inStorDetails, AccountViewModel account)
        {
            try
            {
                var inStorage = Mapper.Map<InStorage>(vm);
                var supId = Convert.ToInt32(vm.SupNum);
                var sup =await _context.Supplier.FirstOrDefaultAsync(o=>o.Id == supId);
                inStorage.SupName = sup.SupName;
                inStorage.Status = (int)InOpStatus.待入库;
                inStorage.Num = inStorDetails.Sum(o=>o.Num);
                inStorage.CreateTime = DateTime.Now;
                inStorage.CreateUser = account.Name;
                inStorage.CompanyId = account.CompanyId;
                inStorage.StorageNum = account.BranchId.ToString();
                await _context.InStorage.AddAsync(inStorage);
                foreach (var inStorDetail in inStorDetails)
                {
                    var entry = Mapper.Map<InStorDetail>(inStorDetail);
                    entry.OrderNum = inStorage.OrderNum;
                    entry.CreateTime = DateTime.Now;
                    entry.BarCode = entry.BarCode;
                    entry.SnNum = Guid.NewGuid().ToString("N");
                    await _context.InStorDetail.AddAsync(entry);
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
        public async Task<ServiceResponseBase> Update(InStorageViewModel vm, List<InStorDetailViewModel> inStorDetails, AccountViewModel account)
        {
            try
            {
                var inStorage = await _context.InStorage.FirstOrDefaultAsync(o => o.Id == vm.Id);
                
                inStorage.InType = vm.InType;
                inStorage.SupNum = vm.SupNum;
                var supId = Convert.ToInt32(vm.SupNum);
                var sup = await _context.Supplier.FirstOrDefaultAsync(o => o.Id == supId);
                inStorage.SupName = sup.SupName;
                inStorage.ContactName = vm.ContactName;
                inStorage.Phone = vm.Phone;
                inStorage.Remark = vm.Remark;
                var status = (InOpStatus)inStorage.Status;
                if (status == InOpStatus.待入库)
                {
                    inStorage.Num = inStorDetails.Sum(o => o.Num);
                }
                _context.Entry(inStorage).State = EntityState.Modified;
                var _inStorDetails = await _context.InStorDetail.Where(o=>o.OrderNum == inStorage.OrderNum).ToListAsync();
                foreach (var _inStorDetail in _inStorDetails)
                {
                    var inStorDetail = inStorDetails.FirstOrDefault(o=>o.ProductNum == _inStorDetail.ProductNum
                        && o.LocalNum == _inStorDetail.LocalNum);
                    if (inStorDetail == null)
                    {
                        _context.InStorDetail.Remove(_inStorDetail);
                    }
                }

                foreach (var inStorDetail in inStorDetails)
                {
                    var _inStorDetail = _inStorDetails.FirstOrDefault(o => o.ProductNum == inStorDetail.ProductNum
                        && o.LocalNum == inStorDetail.LocalNum);
                    if (_inStorDetail != null)
                    {
                        if (status == InOpStatus.待入库)
                        {
                            _inStorDetail.Num = inStorDetail.Num;
                        }
                        if (status == InOpStatus.已入库)
                        {
                            _inStorDetail.RealNum = inStorDetail.Num;
                        }
                        if (status == InOpStatus.已上架)
                        {
                            _inStorDetail.PutRealNum = inStorDetail.Num;
                        }
                        _inStorDetail.InPrice = inStorDetail.InPrice;
                    }
                    else
                    {
                        var entry = Mapper.Map<InStorDetail>(inStorDetail);
                        entry.OrderNum = inStorage.OrderNum;
                        entry.CreateTime = DateTime.Now;
                        entry.BarCode = entry.ProductNum;
                        entry.SnNum = Guid.NewGuid().ToString("N");
                        if (status == InOpStatus.待入库)
                        {
                            entry.Num = inStorDetail.Num;
                        }
                        if (status == InOpStatus.已入库)
                        {
                            entry.RealNum = inStorDetail.Num;
                        }
                        if (status == InOpStatus.已上架)
                        {
                            entry.PutRealNum = inStorDetail.Num;
                        }
                        await _context.InStorDetail.AddAsync(entry);
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
                    var inStorage = await _context.InStorage.FirstOrDefaultAsync(o => o.Id == _id);
                    if (inStorage != null)
                    {
                        inStorage.IsDelete = 1;
                        _context.Entry(inStorage).State = EntityState.Modified;
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
                var inStorage = await _context.InStorage.FirstOrDefaultAsync(o => o.Id == id);
                if (inStorage != null)
                {
                    inStorage.Status += 1;
                    var inStorageDetails = await _context.InStorDetail.Where(o => o.OrderNum == inStorage.OrderNum).ToListAsync();
                    if (inStorage.Status == (int)InOpStatus.已入库)
                    {
                        foreach (var inStorageDetail in inStorageDetails)
                        {
                            inStorageDetail.RealNum = inStorageDetail.Num;
                        }
                        
                    }
                    if (inStorage.Status == (int)InOpStatus.已上架)
                    {
                        foreach (var inStorageDetail in inStorageDetails)
                        {
                            inStorageDetail.PutRealNum = inStorageDetail.RealNum;
                            var localProduct = await _context.LocalProduct.FirstOrDefaultAsync(o => o.CompanyId == inStorage.CompanyId
                                && o.BarCode == inStorageDetail.BarCode && o.LocalNum == inStorageDetail.LocalNum);
                            if (localProduct == null)
                            {
                                var location = await _context.Location.FirstOrDefaultAsync(o=>o.LocalBarCode == inStorageDetail.LocalNum);
                                localProduct = new LocalProduct
                                {
                                    Sn = Guid.NewGuid().ToString("N"),
                                    StorageNum = location.StorageNum,
                                    StorageName = null,
                                    LocalNum = inStorageDetail.LocalNum,
                                    LocalName = null,
                                    LocalType = location.LocalType,
                                    ProductNum = inStorageDetail.ProductNum,
                                    BarCode = inStorageDetail.BarCode,
                                    ProductName = inStorageDetail.ProductName,
                                    BatchNum = "",
                                    Num = (double)inStorageDetail.PutRealNum,
                                    SupNum = null,
                                    SupName = null,
                                    CreateTime = DateTime.Now,
                                    CreateUser = account.Id.ToString(),
                                    CreateName = account.Name,
                                    Remark = null,
                                    CompanyId = account.CompanyId
                                };
                                _context.LocalProduct.Add(localProduct);

                            }
                            else
                            {
                                localProduct.Num += (double)inStorageDetail.PutRealNum;
                            }
                        }
                    }
                    _context.Entry(inStorage).State = EntityState.Modified;
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
