using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Controllers;
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
            var result = await _supplierServices.GetList(GetFilters(), GetSort());
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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