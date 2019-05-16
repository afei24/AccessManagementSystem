using AccessManagementServices.DOTS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementServices
{
    public interface IAccountServices
    {
        Task<List<AccountViewModel>> GetList();
    }
}
