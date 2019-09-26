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
    public class CustomerReportController : BaseController
    {
        private OutStorageServices _outStorageServices;
        private BasicInfoServices _basicInfoServices;
        public CustomerReportController(OutStorageServices outStorageServices, BasicInfoServices basicInfoServices
            , ILogger<CustomerReportController> logger)
            : base(logger)
        {
            _outStorageServices = outStorageServices;
            _basicInfoServices = basicInfoServices;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _outStorageServices.GetListCustomerReport(GetFilters(), GetSort(),GetAccount());
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
                StartDateTime = HttpContext.Request.Query["startTime"],
                EndDateTime = HttpContext.Request.Query["endTime"],
            };
            return filters;
        }

        // GET: CustomerReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerReport/Create
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

        // GET: CustomerReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerReport/Edit/5
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

        // GET: CustomerReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerReport/Delete/5
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