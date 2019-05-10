using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagementData;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccessManagement.Controllers
{
    public class AccountController : Controller
    {
        private AccountServices _accountServices;
        public AccountController(AccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        // GET: Account
        public async Task<ActionResult> Index()
        {
            var vms = await _accountServices.GetList();
            return View(vms);
        }

        //private readonly AccessManagementContext _context;

        //public AccountController(AccessManagementContext context)
        //{
        //    _context = context;
        //}

        //// GET: Accounts
        //public async Task<IActionResult> Index()
        //{
        //    var accessManagementContext = _context.Account.Include(a => a.Branch).Include(a => a.Company);
        //    var res = await accessManagementContext.ToListAsync();
        //    return View(await accessManagementContext.ToListAsync());
        //}

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
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

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
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