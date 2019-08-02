using AccessManagementData;
using AccessManagementServices.Common;
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
            CreateMap<CompanyViewModel, Company>()
                .ForMember(c => c.Status, conf => conf.MapFrom(s => (ComapnyStatus)s.Status));
            CreateMap<Company, CompanyViewModel>().
                ForMember(c=>c.StatusName,conf=>conf.MapFrom(s=>s.Status.ToString()))
                .ForMember(c => c.Status, conf => conf.MapFrom(s => (int)s.Status));

            CreateMap<AppMenuViewModel, AppMenu>();
            CreateMap<AppMenu, AppMenuViewModel>();

            CreateMap<BranchViewModel, Branch>();
            CreateMap<Branch, BranchViewModel>()
                .ForMember(d => d.ParentBranchName, conf => conf.MapFrom(s => s.ParentBranch.Name));

            CreateMap<PresetFunctionViewModel, ReSetFunction>();
            CreateMap<ReSetFunction, PresetFunctionViewModel>();

            CreateMap<AccountViewModel, Account>();
            CreateMap<Account, AccountViewModel>().
                ForMember(c => c.StatusName, conf => conf.MapFrom(s => s.Status.ToString()));

            CreateMap<RoleViewModel, Role>()
            .ForMember(d => d.AccountRole, conf => conf.Ignore())
            .ForMember(d => d.Company, conf => conf.Ignore())
            .ForMember(d => d.FunctionRole, conf => conf.Ignore());
            CreateMap<Role, RoleViewModel>()
                .ForMember(d => d.Functions, conf => conf.Ignore());
        }
    }
}
