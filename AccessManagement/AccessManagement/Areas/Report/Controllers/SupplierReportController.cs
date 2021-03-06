﻿using System;
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
    public class SupplierReportController : BaseController
    {
        private InStorageServices _inStorageServices;
        private BasicInfoServices _basicInfoServices;
        public SupplierReportController(InStorageServices inStorageServices, BasicInfoServices basicInfoServices
            , ILogger<SupplierReportController> logger)
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
            var result = await _inStorageServices.GetListSupReport(GetFilters(), GetSort(),GetAccount());
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
                Status = HttpContext.Request.Query["status"],
                StartDateTime = HttpContext.Request.Query["startTime"],
                EndDateTime = HttpContext.Request.Query["endTime"],
            };
            return filters;
        }

        // GET: SupplierReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SupplierReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierReport/Create
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

        // GET: SupplierReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SupplierReport/Edit/5
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

        // GET: SupplierReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SupplierReport/Delete/5
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