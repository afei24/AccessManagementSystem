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
    public class InStorageController : BaseController
    {
        private InStorageServices _inStorageServices;
        private BasicInfoServices _basicInfoServices;
        public InStorageController(InStorageServices inStorageServices, BasicInfoServices basicInfoServices
            , ILogger<InStorageController> logger)
            : base(logger)
        {
            _inStorageServices = inStorageServices;
            _basicInfoServices = basicInfoServices;
        }

        // GET: IMS/Location
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _inStorageServices.GetList(GetFilters(), GetSort());
            return Json(result);
        }
        public InStorageFilters GetFilters()
        {
            var filters = new InStorageFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                OrderNum = HttpContext.Request.Query["orderNum"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }

        // GET: InStorage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InStorage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InStorage/Create
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

        // GET: InStorage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InStorage/Edit/5
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

        // GET: InStorage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InStorage/Delete/5
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