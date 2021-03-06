﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Filter;
using AccessManagement.Middleware;
using AccessManagementData;
using AccessManagementServices;
using AccessManagementServices.DOTS;
using AccessManagementServices.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using WMSData;

namespace AccessManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddMvc(
                options =>
                {
                    options.Filters.Add<HttpGlobalExceptionFilter>(); //加入全局异常类
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<AccessManagementContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                providerOptions => providerOptions.EnableRetryOnFailure()));
            services.AddDbContext<LuJCDBContext>(options =>
                options.UseSqlServer(Configuration["WMSConnectionStrings:DefaultConnection"],
                providerOptions => providerOptions.EnableRetryOnFailure()));
            services.AddTransient<CompanyServices, CompanyServices>();
            services.AddTransient<BasicInfoServices, BasicInfoServices>();
            services.AddTransient<AppMenuServices>();
            services.AddTransient<IAccountServices,AccountServices>();
            services.AddTransient<BranchServices>();
            services.AddTransient<PresetFunctionServices>();
            services.AddTransient<RoleServices>();
            services.AddTransient<LocationServices>();
            services.AddTransient<SupplierServices>();
            services.AddTransient<CustomerServices>();
            services.AddTransient<MeasureServices>();
            services.AddTransient<ProductCategoryServices>();
            services.AddTransient<ProductServices>();
            services.AddTransient<InStorageServices>();
            services.AddTransient<OutStorageServices>(); 
            services.AddTransient<BadReportServices>();
            services.AddTransient<CheckStockServices>();
            services.AddTransient<LocalProductServices>();
            //services.AddScoped<AddHeaderFilterWithDI>();
            services.AddAutoMapper(typeof(Startup));
            Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<AccessManagementProfile>();
                }
            );
            //Mapper.Initialize(cfg => cfg.CreateMap<AppMenu, AppMenuViewModel>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            app.UseSession();
            
            loggerFactory.AddNLog(); //添加NLog
            NLog.LogManager.LoadConfiguration("nlog.config");
            app.UseLog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseAccountSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapAreaRoute("IMS", "IMS", "IMS/{controller}/{action}/{id?}",
                    defaults: new { Controller = "Location", Action = "Index" });
                routes.MapAreaRoute("WMS", "WMS", "WMS/{controller}/{action}/{id?}",
                    defaults: new { Controller = "InStorage", Action = "Index" });
                routes.MapAreaRoute("Report", "Report", "Report/{controller}/{action}/{id?}",
                    defaults: new { Controller = "LocalProduct", Action = "Index" });
            });
            
        }
    }
}
