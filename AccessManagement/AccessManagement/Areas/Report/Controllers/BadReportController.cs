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

namespace AccessManagement.Areas.Report.Controllers
{
    public class BadReportController : BaseController
    {
        private BadReportServices _badReportServices;
        private BasicInfoServices _basicInfoServices;
        public BadReportController(BadReportServices badReportServices, BasicInfoServices basicInfoServices
            , ILogger<BadReportController> logger)
            : base(logger)
        {
            _badReportServices = badReportServices;
            _basicInfoServices = basicInfoServices;
        }

        // GET: IMS/Location
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _badReportServices.GetList(GetFilters(), GetSort());
            return Json(result);
        }
        public BadReportFilters GetFilters()
        {
            var filters = new BadReportFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                OrderNum = HttpContext.Request.Query["orderNum"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }

        // GET: BadReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BadReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BadReport/Create
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

        // GET: BadReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BadReport/Edit/5
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

        // GET: BadReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BadReport/Delete/5
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