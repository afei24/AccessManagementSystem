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
            var result = await _measureServices.GetList(GetFilters(), GetSort(),GetAccount());
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

        public async Task<IActionResult> Create()
        {
            var vm = new MeasureViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeasureViewModel vm)
        {
            var result = await _measureServices.Create(vm, GetAccount());
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
            var vm = await _measureServices.GetById((int)id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MeasureViewModel vm)
        {
            var result = await _measureServices.Update(vm, GetAccount());
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
        public async Task<ActionResult> DeleteIds(string ids)
        {
            try
            {
                var result = await _measureServices.Delete(ids);
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