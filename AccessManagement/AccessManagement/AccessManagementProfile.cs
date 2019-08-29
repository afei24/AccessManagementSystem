using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.DOTS.WMS.IMS;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMSData;

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

            CreateMap<AccountViewModel, Account>().
            ForMember(c => c.Status, conf => conf.MapFrom(s => Enum.Parse(typeof(AccountStatus), s.StatusName)));
            CreateMap<Account, AccountViewModel>().
                ForMember(c => c.StatusName, conf => conf.MapFrom(s => s.Status.ToString())).
                ForMember(c => c.CreateTimeStr, conf => conf.MapFrom(s => s.CreateTime.ToString("yyyy-MM-dd hh:mm"))).
                ForMember(c => c.BranchName, conf => conf.MapFrom(s => s.Branch.Name));

            CreateMap<RoleViewModel, Role>()
            .ForMember(d => d.AccountRole, conf => conf.Ignore())
            .ForMember(d => d.Company, conf => conf.Ignore())
            .ForMember(d => d.FunctionRole, conf => conf.Ignore());
            CreateMap<Role, RoleViewModel>()
                .ForMember(d => d.Functions, conf => conf.Ignore());
            CreateMap<LocationViewModel, Location>();
            CreateMap<Location, LocationViewModel>()
                .ForMember(d => d.LocalTypeStr, conf => conf.MapFrom(s => (Enum.Parse(typeof(LocalType), s.LocalType.ToString())).ToString()));

            CreateMap<SupplierViewModel, Supplier>();
            CreateMap<Supplier, SupplierViewModel>()
                .ForMember(d => d.SupTypeStr, conf => conf.MapFrom(s => (Enum.Parse(typeof(SupType), s.SupType.ToString())).ToString()));
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.LocalName, conf => conf.Ignore())
                 .ForMember(d => d.LocalProductNum, conf => conf.Ignore())
                  .ForMember(d => d.InStorageNum, conf => conf.Ignore())
                   .ForMember(d => d.InStorageNumPCT, conf => conf.Ignore())
                    .ForMember(d => d.OutStorageNum, conf => conf.Ignore())
                     .ForMember(d => d.OutStorageNumPCT, conf => conf.Ignore())
                      .ForMember(d => d.BadReportNum, conf => conf.Ignore())
                       .ForMember(d => d.TotalLocalProductNum, conf => conf.Ignore())
                        .ForMember(d => d.TotalInStorageNum, conf => conf.Ignore())
                         .ForMember(d => d.TotalOutStorageNum, conf => conf.Ignore())
                          .ForMember(d => d.TotalBadReportNum, conf => conf.Ignore())
                           .ForMember(d => d.Branchs, conf => conf.Ignore())
                            .ForMember(d => d.Goods, conf => conf.Ignore())
                             .ForMember(d => d.Customers, conf => conf.Ignore())
                              .ForMember(d => d.Measures, conf => conf.Ignore());
        }
    }
}
