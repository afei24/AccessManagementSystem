using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Controllers;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS.WMS.IMS;
using AccessManagementServices.Filters;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccessManagement.Areas.IMS.Controllers
{
    [Area("IMS")]
    public class SupplierController : BaseController
    {
        private SupplierServices _supplierServices;
        private BasicInfoServices _basicInfoServices;
        public SupplierController(SupplierServices supplierServices, BasicInfoServices basicInfoServices
            , ILogger<SupplierController> logger)
            : base(logger)
        {
                _supplierServices = supplierServices;
            _basicInfoServices = basicInfoServices;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _supplierServices.GetList(GetFilters(), GetSort(),GetAccount());
            return Json(result);
        }
        public SupplierFilter GetFilters()
        {
            var filters = new SupplierFilter()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel vm)
        {
            var result = await _supplierServices.Create(vm, GetAccount());
            if (result.Status == Status.ok)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "保存失败: " + result.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var vm = await _supplierServices.GetById((int)id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupplierViewModel vm)
        {
            var result = await _supplierServices.Update(vm, GetAccount());
            if (result.Status == Status.ok)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "保存失败: " + result.Message);
                return View(vm);
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}