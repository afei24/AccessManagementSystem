using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Helper;
using AccessManagementData;
using AccessManagementServices;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccessManagement.Controllers
{
    public class AccountController : Controller
    {
        IMapper _mapper;
        private IAccountServices _accountServices;
        private AccessManagementContext _context;
        public AccountController(IAccountServices accountServices, IMapper mapper, AccessManagementContext context)
        {
            _accountServices = accountServices;
            _mapper = mapper;
            _context = context;
    }
        // GET: Account
        public async Task<ActionResult> Index()
        {
            var vms = await _accountServices.GetList();
            return View(vms);
        }

        public ActionResult Login()
        {
            return View(new AccountViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Login(AccountViewModel vm)
        {
            var account =  await _accountServices.Login(vm);
            if (account != null)
            {
                var vmAccount = _mapper.Map<AccountViewModel>(account);
                var branch = _mapper.Map<BranchViewModel>(account.Branch);
                var company = _mapper.Map<CompanyViewModel>(account.Company);
                var functionIds = account.AccountFunction.Select(o=>o.FunctionId).ToList();
                var functions = _context.Function.Where(o=>functionIds.Contains(o.Id)).ToList();
                foreach (var role in account.AccountRole)
                {
                    var roleFunctionIds = role.Role.FunctionRole.Select(o=>o.FunctionId).ToList();
                    var _functions = _context.Function.Where(o=> roleFunctionIds.Contains(o.Id)).ToList();
                    functions = functions.Union(_functions).ToList();
                }
                HttpContext.Session.Set("account", SerializeHelper.SerializeToBinary(vmAccount));
                HttpContext.Session.Set("branch", SerializeHelper.SerializeToBinary(branch));
                HttpContext.Session.Set("company", SerializeHelper.SerializeToBinary(company));
                HttpContext.Session.Set("functions", SerializeHelper.SerializeToBinary(functions));
                return Redirect("Index");
            }
            ModelState.AddModelError("", "用户名或密码错误。");
            return View();
        }


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