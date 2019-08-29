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
    public class GoodsController : BaseController
    {
        private ProductCategoryServices _productCategoryServices;
        private BasicInfoServices _basicInfoServices;
        public GoodsController(ProductCategoryServices productCategoryServices, BasicInfoServices basicInfoServices
            , ILogger<GoodsController> logger)
            : base(logger)
        {
            _productCategoryServices = productCategoryServices;
            _basicInfoServices = basicInfoServices;
        }

        // GET: IMS/Location
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _productCategoryServices.GetList(GetFilters(), GetSort(),GetAccount());
            return Json(result);
        }
        public ProductCategoryFilters GetFilters()
        {
            var filters = new ProductCategoryFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }
        // GET: Goods/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var vm = new ProductCategoryViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategoryViewModel vm)
        {
            var result = await _productCategoryServices.Create(vm, GetAccount());
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
            var vm = await _productCategoryServices.GetById((int)id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCategoryViewModel vm)
        {
            var result = await _productCategoryServices.Update(vm, GetAccount());
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

        // GET: Goods/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Goods/Delete/5
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
                var result = await _productCategoryServices.Delete(ids);
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