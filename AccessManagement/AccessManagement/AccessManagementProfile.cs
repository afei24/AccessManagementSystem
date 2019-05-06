using AccessManagementData;
using AccessManagementServices.DOTS;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement
{
    public class AccessManagementProfile: Profile
    {
        public AccessManagementProfile()
        {
            CreateMap<CompanyViewModel, Company>();
            CreateMap<Company, CompanyViewModel>();
        }
    }
}
