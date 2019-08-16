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
    public class MeasureController : BaseController
    {
        private MeasureServices _measureServices;
        private BasicInfoServices _basicInfoServices;
        public MeasureController(MeasureServices measureServices, BasicInfoServices basicInfoServices
            , ILogger<MeasureController> logger)
            : base(logger)
        {
            _measureServices = measureServices;
            _basicInfoServices = basicInfoServices;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _measureServices.GetList(GetFilters(), GetSort());
            return Json(result);
        }
        public MeasureFilters GetFilters()
        {
            var filters = new MeasureFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }

        // GET: Measure/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Measure/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Measure/Create
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

        // GET: Measure/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Measure/Edit/5
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

        // GET: Measure/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Measure/Delete/5
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