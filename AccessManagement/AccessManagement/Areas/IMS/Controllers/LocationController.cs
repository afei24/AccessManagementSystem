using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Controllers;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.Filters;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WMSData;

namespace AccessManagement.Areas.IMS.Controllers
{
    [Area("IMS")]
    public class LocationController : BaseController
    {
        private LocationServices _locationServices;
        private BasicInfoServices _basicInfoServices;
        public LocationController(LocationServices locationServices, BasicInfoServices basicInfoServices
            , ILogger<LocationController> logger)
            : base(logger)
        {
            _locationServices = locationServices;
            _basicInfoServices = basicInfoServices;
        }

        // GET: IMS/Location
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _locationServices.GetList(GetFilters(), GetSort(), GetAccount());
            var branchs =await _basicInfoServices.GetBranchs(GetAccount());
            foreach (var vm in result.data)
            {
                var branchId = vm.BranchId.ToString();
                vm.StorageNum = branchs.FirstOrDefault(o=>o.Value == branchId).Text;
            }
            return Json(result);
        }
        public LocationFilters GetFilters()
        {
            var filters = new LocationFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }

        // GET: IMS/Location/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }

        // GET: IMS/Location/Create
        public async Task<IActionResult> Create()
        {
            var vm = new LocationViewModel();
            await Init(vm);
            return View(vm);
        }

        // POST: IMS/Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationViewModel vm)
        {
            var result = await _locationServices.Create(vm, GetAccount());
            if (result.Status == Status.ok)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "保存失败: " + result.Message);
                await Init(vm);
                return View(vm);
            }
        }

        // GET: IMS/Location/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var vm = await _locationServices.GetById((int)id);
            await Init(vm);
            return View(vm);
        }

        // POST: IMS/Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LocationViewModel vm)
        {
            var result = await _locationServices.Update(vm, GetAccount());
            if (result.Status == Status.ok)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "保存失败: " + result.Message);
                await Init(vm);
                return View(vm);
            }
        }

        // GET: IMS/Location/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            return View();
        }

        // POST: IMS/Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> DeleteIds(string ids)
        {
            try
            {
                var result = await _locationServices.Delete(ids);
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

        public async Task Init(LocationViewModel vm)
        {
            vm.Branchs =await _basicInfoServices.GetBranchs(GetAccount());
        }

    }
}
