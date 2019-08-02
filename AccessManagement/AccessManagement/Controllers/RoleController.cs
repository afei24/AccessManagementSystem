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
using Microsoft.Extensions.Logging;

namespace AccessManagement.Controllers
{
    public class RoleController : BaseController
    {
        private RoleServices _roleServices;
        public RoleController(RoleServices roleServices, ILogger<RoleController> logger)
            : base(logger)
        {
            _roleServices = roleServices;
        }
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _roleServices.GetList(GetFilters(), GetSort());
            return Json(result);
        }
        public RoleFilters GetFilters()
        {
            var filters = new RoleFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
            };
            return filters;
        }

        // GET: Role/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var vm = await _roleServices.GetById(id);
            return View(vm);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View(new RoleViewModel());
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleViewModel vm)
        {
            var result = await _roleServices.Create(vm, GetAccount());
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

        // GET: Role/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var vm = await _roleServices.GetById(id);
            return View(vm);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RoleViewModel vm)
        {
            var result = await _roleServices.Update(vm, GetAccount());
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

        public async Task<ActionResult> Set(int id)
        {
            var vm = await _roleServices.GetById(id);
            ViewData["Id"] = vm.Id;
            ViewData["Name"] = vm.Name;
            return View(vm);
        }

        [HttpPost]
        [Route("Role/Set")]
        public async Task<ActionResult> Set([FromForm]TreeData models)
        {
            var result = await _roleServices.UpdateFunctionTree(models, GetAccount());
            if (result.Status == Status.ok)
            {
                return Json("ok");
            }
            else
            {
                return Json(result.Message);
            }
        }

        public async Task<List<TreeDataModel>> GetFunctions(int id)
        {
            return await _roleServices.GenerateTree(id,GetAccount());
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Role/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RoleViewModel collection)
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
                var result = await _roleServices.Delete(ids);
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