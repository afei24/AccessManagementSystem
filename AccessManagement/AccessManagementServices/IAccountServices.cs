using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.Filters;
using AccessManagementServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementServices
{
    public interface IAccountServices
    {
        Task<List<AccountViewModel>> GetList();
        Task<Account> Login(AccountViewModel vm);
        Task<ResponseModel<AccountViewModel>> GetList(AccountFilters filters, SortCol sortCol, AccountViewModel current);
        Task<ServiceResponseBase> Create(AccountViewModel vm, AccountViewModel current);
        Task<ServiceResponseBase> Delete(string idStr);
        Task<AccountViewModel> GetById(int id);
        Task<ServiceResponseBase> Update(AccountViewModel vm);
    }
}
