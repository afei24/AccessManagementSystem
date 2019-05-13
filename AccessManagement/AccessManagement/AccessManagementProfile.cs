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
            CreateMap<AppMenuViewModel, AppMenu>();
            CreateMap<AppMenu, AppMenuViewModel>();
            CreateMap<BranchViewModel, Branch>();
            CreateMap<Branch, BranchViewModel>()
                .ForMember(d => d.ParentBranchName, conf => conf.MapFrom(s => s.ParentBranch.Name)); ;
        }
    }
}
