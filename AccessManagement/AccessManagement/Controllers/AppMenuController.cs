using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagementServices.DOTS;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.Controllers
{
    public class AppMenuController : Controller
    {
        private AppMenuServices _appMenuServices;
        public AppMenuController(AppMenuServices appMenuServices)
        {
            _appMenuServices = appMenuServices;
        }
        // GET: AppMenu
        public async Task<ActionResult> Index()
        {
            var vms = await _appMenuServices.GetList();
            return View(vms);
        }

        // GET: AppMenu/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var vm = await _appMenuServices.GetById(id);
            return View(vm);
        }

        // GET: AppMenu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppMenu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppMenuViewModel vm)
        {
            try
            {
                var result = await _appMenuServices.Create(vm);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppMenu/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var vm = await _appMenuServices.GetById(id);
            return View(vm);
        }

        // POST: AppMenu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AppMenuViewModel vm)
        {
            try
            {
                var result = await _appMenuServices.Update(vm);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppMenu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppMenu/Delete/5
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