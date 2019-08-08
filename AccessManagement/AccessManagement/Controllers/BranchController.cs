using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.Filters;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AccessManagement.Controllers
{
    public class BranchController : BaseController
    {
        private BranchServices _branchServices;
        private BasicInfoServices _basicInfoServices;
        public BranchController(BranchServices branchServices, ILogger<BranchController> logger
            , BasicInfoServices basicInfoServices)
            : base(logger)
        {
            _branchServices = branchServices;
            _basicInfoServices = basicInfoServices;
        }
        // GET: Branch
        public async Task<ActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _branchServices.GetList(GetFilters(), GetSort(), GetAccount());
            return Json(result);
        }
        public BranchFilters GetFilters()
        {
            var filters = new BranchFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
            };
            return filters;
        }

        // GET: Branch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Branch/Create
        public async Task<ActionResult> Create()
        {
            var vm = new BranchViewModel();
            await Init(vm);
            return View(vm);
        }

        // POST: Branch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BranchViewModel vm)
        {
            var result = await _branchServices.Create(vm, GetAccount());
            if (result.Status == Status.ok)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "保存失败: " + result.Message);
                await Init(vm);
                return View(vm);
            }
        }

        // GET: Branch/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var vm = await _branchServices.GetById(id);
                await Init(vm);
                return View(vm);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: Branch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BranchViewModel vm)
        {
            var result = await _branchServices.Update(vm,GetAccount());
            if (result.Status == Status.ok)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "保存失败: " + result.Message);
                await Init(vm);
                return View(vm);
            }
        }

        // GET: Branch/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Branch/Delete/5
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
                var result = await _branchServices.Delete(ids);
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

        private async Task Init(BranchViewModel vm)
        {
            var branchs = await _basicInfoServices.GetBranchs(GetAccount());
            if (vm.Id != 0)
            {
                branchs = branchs.Where(o=> o.Text != vm.Name).ToList();
            }
            branchs.Insert(0, new SelectListItem() { Text = "", Value = "" });
            ViewBag.Branchs = branchs;
        }
    }
}