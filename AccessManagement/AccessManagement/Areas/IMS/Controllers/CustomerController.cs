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
    public class CustomerController : BaseController
    {
        private CustomerServices _customerServices;
        private BasicInfoServices _basicInfoServices;
        public CustomerController(CustomerServices customerServices, BasicInfoServices basicInfoServices
            , ILogger<CustomerController> logger)
            : base(logger)
        {
            _customerServices = customerServices;
            _basicInfoServices = basicInfoServices;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _customerServices.GetList(GetFilters(), GetSort(), GetAccount());
            return Json(result);
        }
        public CustomerFilters GetFilters()
        {
            var filters = new CustomerFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            var vm = new CustomerViewModel();
            return View(vm);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerViewModel vm)
        {
            var result = await _customerServices.Create(vm, GetAccount());
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

        // GET: Customer/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var vm = await _customerServices.GetById(id);
            return View(vm);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CustomerViewModel vm)
        {
            var result = await _customerServices.Update(vm, GetAccount());
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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
                var result = await _customerServices.Delete(ids);
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