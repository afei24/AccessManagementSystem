using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Helper;
using AccessManagementData;
using AccessManagementServices;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.Filters;
using AccessManagementServices.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WMSData;

namespace AccessManagement.Controllers
{
    public class AccountController : BaseController
    {
        IMapper _mapper;
        private IAccountServices _accountServices;
        private AccessManagementContext _context;
        private PresetFunctionServices _presetFunctionServices;
        private BasicInfoServices _basicInfoServices;
        private LuJCDBContext _luJCDBContext;
        public AccountController(IAccountServices accountServices, IMapper mapper, AccessManagementContext context
            ,PresetFunctionServices presetFunctionServices, ILogger<AccountController> logger
            , BasicInfoServices basicInfoServices, LuJCDBContext luJCDBContext)
            : base(logger)
        {
            _accountServices = accountServices;
            _mapper = mapper;
            _context = context;
            _presetFunctionServices = presetFunctionServices;
            _basicInfoServices = basicInfoServices;
            _luJCDBContext = luJCDBContext;
    }
        // GET: Account
        public async Task<ActionResult> Index()
        {
            //var vms = await _accountServices.GetList();
            return View();
        }

        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _accountServices.GetList(GetFilters(), GetSort(),GetAccount());
            return Json(result);
        }
        public AccountFilters GetFilters()
        {
            var filters = new AccountFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
            };
            return filters;
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
                foreach (var accountRole in account.AccountRole)
                {
                    var _functionIds =await _context.FunctionRole.Where(o=>o.RoleId == accountRole.RoleId)
                        .Select(o=>o.FunctionId).ToListAsync();
                    var _functions = _context.Function.Where(o=> _functionIds.Contains(o.Id)).ToList();
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
        public async Task<ActionResult> Create()
        {
            var vm = new AccountViewModel();
            await Init(vm);
            return View(vm);
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccountViewModel vm)
        {
            var result = await _accountServices.Create(vm, GetAccount()); 
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

        // GET: Account/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var vm = await _accountServices.GetById(id);
                await Init(vm);
                return View(vm);
            }
            catch (Exception ex)
            {
                return View();
            } 
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AccountViewModel vm)
        {
            var result = await _accountServices.Update(vm);
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
        public async Task<ActionResult> DeleteIds(string ids)
        {
            try
            {
                var result = await _accountServices.Delete(ids);
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

        private async Task Init(AccountViewModel vm)
        {
            ViewBag.Roles =await _basicInfoServices.GetRoles(GetAccount());
            ViewBag.Branchs = await _basicInfoServices.GetBranchs(GetAccount());
        }
    }
}