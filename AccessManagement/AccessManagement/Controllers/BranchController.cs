using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.Controllers
{
    public class BranchController : Controller
    {
        private BranchServices _branchServices;
        public BranchController(BranchServices branchServices)
        {
            _branchServices = branchServices;
        }
        // GET: Branch
        public async Task<ActionResult> Index()
        {
            var vms =await  _branchServices.GetList();
            return View(vms);
        }

        // GET: Branch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Branch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branch/Create
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

        // GET: Branch/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Branch/Edit/5
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
    }
}