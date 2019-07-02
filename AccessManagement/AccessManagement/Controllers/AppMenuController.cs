using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Models;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.Filters;
using AccessManagementServices.Models;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AccessManagement.Controllers
{
    public class AppMenuController : BaseController
    {
        private AppMenuServices _appMenuServices;
        private BasicInfoServices _basicInfoServices;
        public AppMenuController(AppMenuServices appMenuServices, BasicInfoServices basicInfoServices
            , ILogger<AppMenuController> logger)
            : base(logger)
        {
            _appMenuServices = appMenuServices;
            _basicInfoServices = basicInfoServices;
        }
        // GET: AppMenu
        public async Task<ActionResult> Index()
        {
            //var vms = await _appMenuServices.GetList();
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _appMenuServices.GetList(GetFilters(), GetSort());
            return Json(result);
        }
        public AppmenuFilters GetFilters()
        {
            var filters = new AppmenuFilters() {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }

        // GET: AppMenu/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var vm = await _appMenuServices.GetById(id);
            return View(vm);
        }

        // GET: AppMenu/Create
        public async Task<ActionResult> Create()
        {
            var vm = new AppMenuViewModel();
            await Init(vm);
            return View(vm);
        }

        // POST: AppMenu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppMenuViewModel vm)
        {
            try
            {
                var result = await _appMenuServices.Create(vm);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppMenu/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var vm = await _appMenuServices.GetById(id);
            return View(vm);
        }

        // POST: AppMenu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AppMenuViewModel vm)
        {
            try
            {
                var result = await _appMenuServices.Update(vm);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppMenu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        public async Task<ActionResult> DeleteIds(string ids)
        {
            try
            {
                var result =await _appMenuServices.Delete(ids);
                if (result.Status == Status.ok)
                    return Json("ok");
                else
                    return Json(result.Message);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string idsStr)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task Init(AppMenuViewModel vm)
        {
            vm.ParentAppMenus =  (await _basicInfoServices.GetParentAppMenu())
                .Select(a=>new SelectListItem() { Text = a.Name,Value = a.Id.ToString()})
                .ToList();
        }
    }
}