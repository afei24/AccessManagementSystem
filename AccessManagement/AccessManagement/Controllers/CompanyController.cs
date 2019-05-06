using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagementServices.DOTS;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace AccessManagement.Controllers
{
    public class CompanyController : BaseController
    {
        private CompanyServices _companyServices;
        private BasicInfoServices _basicInfoServices;
        public CompanyController(CompanyServices companyServices
            , BasicInfoServices basicInfoServices
            , ILogger<CompanyController> logger)
            : base(logger)
        {
            _companyServices = companyServices;
            _basicInfoServices = basicInfoServices;
        }
        // GET: Company
        public async Task<ActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Index");
                var vms = await _companyServices.GetList();
                return View(vms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                return View();
            }
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public async Task<ActionResult> Create()
        {
            var vm = new CompanyViewModel();
            await Init(vm);
            return View(vm);
        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompanyViewModel vm)
        {
            try
            {
                await _companyServices.Create(vm);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CompanyViewModel collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View();
            }
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Company/Delete/5
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

        public async Task Init(CompanyViewModel vm)
        {
            vm.ComapnyStatuss = _basicInfoServices.GetCompanyStatus();
        }
    }
}