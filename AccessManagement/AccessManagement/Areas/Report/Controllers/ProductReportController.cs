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
    public class ProductReportController : BaseController
    {
        private ProductServices _productServices;
        private BasicInfoServices _basicInfoServices;
        public ProductReportController(ProductServices productServices, BasicInfoServices basicInfoServices
            , ILogger<ProductReportController> logger)
            : base(logger)
        {
            _productServices = productServices;
            _basicInfoServices = basicInfoServices;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _productServices.GetList(GetFilters(), GetSort(),GetAccount());
            return Json(result);
        }
        public ProductFilters GetFilters()
        {
            var filters = new ProductFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }

        // GET: ProductReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductReport/Create
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

        // GET: ProductReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductReport/Edit/5
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

        // GET: ProductReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductReport/Delete/5
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