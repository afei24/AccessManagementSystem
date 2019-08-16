﻿using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
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
    public class LocationServices: BaseServices
    {
        private LuJCDBContext _context;
        public LocationServices(LuJCDBContext context, ILogger<LocationServices> logger)
            : base(logger)
        {
            _context = context;
        }
        public async Task<ResponseModel<LocationViewModel>> GetList(LocationFilters filters, SortCol sortCol)
        {
            var query = _context.Location.Where(o => o.Id != 0);
            query = Search(query, filters);
            query = Sort(query, sortCol);
            var vms = await query.Skip((filters.Page - 1) * filters.Limit).Take(filters.Limit)
                .ProjectTo<LocationViewModel>().ToListAsync();
            ResponseModel<LocationViewModel> result = new ResponseModel<LocationViewModel>();
            result.status = 0;
            result.message = "";
            result.total = query.Count();
            result.data = vms;
            return result;
        }
        public async Task<LocationViewModel> GetById(int id)
        {
            try
            {
                var query = await _context.Location.FirstOrDefaultAsync(o => o.Id == id);
                var vm = Mapper.Map<LocationViewModel>(query);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IQueryable<Location> Search(IQueryable<Location> query, LocationFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(o => o.LocalName.Contains(filters.Name));
            }
            return query;
        }
        public IQueryable<Location> Sort(IQueryable<Location> query, SortCol sortCol)
        {
            switch (sortCol.Field)
            {
                case "id":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                    break;
                case "localName":
                    query = sortCol.Type == "desc" ? query.OrderBy(o => o.LocalName) :
                        query.OrderByDescending(o => o.LocalName);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }
            return query;
        }

        public async Task<ServiceResponseBase> Create(LocationViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.Location.AnyAsync(o => o.LocalName == vm.LocalName
                     && o.CompanyId == account.CompanyId);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复库位" };
                }
                //vm.CompanyId = account.CompanyId;
                var location = Mapper.Map<Location>(vm);
                await _context.Location.AddAsync(location);
                await _context.SaveChangesAsync();
                return new ServiceResponseBase() { Status = Status.ok };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ServiceResponseBase() { Status = Status.error, Message = ex.Message };
            }
        }
        public async Task<ServiceResponseBase> Update(LocationViewModel vm, AccountViewModel account)
        {
            try
            {
                var isExist = await _context.Location.AnyAsync(o => o.LocalName == vm.LocalName
                     && o.CompanyId == account.CompanyId && o.Id != vm.Id);
                if (isExist)
                {
                    return new ServiceResponseBase() { Status = Status.error, Message = "存在重复角色" };
                }
                var location = await _context.Location.FirstOrDefaultAsync(o => o.Id == vm.Id);
                Mapper.Map(vm, location);
                _context.Entry(location).State = EntityState.Modified;
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
                    var location = await _context.Location.FirstOrDefaultAsync(o => o.Id == _id);
                    if (location != null)
                    {
                        _context.Entry(location).State = EntityState.Deleted;
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