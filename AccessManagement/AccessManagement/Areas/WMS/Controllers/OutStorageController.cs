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

namespace AccessManagement.Areas.WMS.Controllers
{
    public class OutStorageController : BaseController
    {
        private OutStorageServices _outStorageServices;
        private BasicInfoServices _basicInfoServices;
        public OutStorageController(OutStorageServices outStorageServices, BasicInfoServices basicInfoServices
            , ILogger<OutStorageController> logger)
            : base(logger)
        {
            _outStorageServices = outStorageServices;
            _basicInfoServices = basicInfoServices;
        }

        // GET: IMS/Location
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _outStorageServices.GetList(GetFilters(), GetSort());
            return Json(result);
        }
        public OutStorageFilters GetFilters()
        {
            var filters = new OutStorageFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                OrderNum = HttpContext.Request.Query["orderNum"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }

        // GET: OutStorage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OutStorage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OutStorage/Create
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

        // GET: OutStorage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OutStorage/Edit/5
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

        // GET: OutStorage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OutStorage/Delete/5
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