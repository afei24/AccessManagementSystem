using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Controllers;
using AccessManagementServices.Common;
using AccessManagementServices.Filters;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccessManagement.Areas.Report.Controllers
{
    public class LocalProductController : BaseController
    {
        private LocalProductServices _localProductServices;
        private BasicInfoServices _basicInfoServices;
        public LocalProductController(LocalProductServices localProductServices, BasicInfoServices basicInfoServices
            , ILogger<LocalProductController> logger)
            : base(logger)
        {
            _localProductServices = localProductServices;
            _basicInfoServices = basicInfoServices;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _localProductServices.GetList(GetFilters(), GetSort(),GetAccount());
            return Json(result);
        }
        public LocalProductFilters GetFilters()
        {
            var filters = new LocalProductFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                LocalNum = HttpContext.Request.Query["localNum"],
                ProductNum = HttpContext.Request.Query["productNum"],
            };
            return filters;
        }

        // GET: LocalProduct/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LocalProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocalProduct/Create
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

        // GET: LocalProduct/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LocalProduct/Edit/5
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

        // GET: LocalProduct/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocalProduct/Delete/5
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

        public async Task<ActionResult> DeleteIds(string ids)
        {
            try
            {
                var result = await _localProductServices.Delete(ids);
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
    }
}