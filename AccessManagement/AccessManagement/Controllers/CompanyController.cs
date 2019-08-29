using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Filter;
using AccessManagement.Models;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.Filters;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;

namespace AccessManagement.Controllers
{
    [Access(FunctionCode.CompanyManagement)]
    public class CompanyController : BaseController
    {
        private CompanyServices _companyServices;
        private PresetFunctionServices _presetFunctionServices;
        private BasicInfoServices _basicInfoServices;
        public CompanyController(CompanyServices companyServices
            , BasicInfoServices basicInfoServices
            , ILogger<CompanyController> logger
            , PresetFunctionServices presetFunctionServices)
            : base(logger)
        {
            _companyServices = companyServices;
            _basicInfoServices = basicInfoServices;
            _presetFunctionServices = presetFunctionServices;
        }
        // GET: Company
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _companyServices.GetList(GetFilters(), GetSort(), GetAccount());
            return Json(result);
        }
        public CompanyFilters GetFilters()
        {
            var filters = new CompanyFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
            };
            return filters;
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
            var result = await _companyServices.Create(vm, GetAccount());
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

        // GET: Company/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var vm =  await _companyServices.GetById(id);
            return View(vm);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CompanyViewModel vm)
        {
            var result = await _companyServices.Update(vm);
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

        public async Task<ActionResult> Set(int id)
        {
            var vm = await _companyServices.GetById(id);
            //var trees =  await GenerateTree(id);
            ViewData["Id"] = vm.Id;
            ViewData["Name"] = vm.Name;
            //var data = JsonConvert.SerializeObject(trees);
            //ViewData["Trees"] = trees;
            return View(vm);
        }
        [HttpPost]
        [Route("Company/Set")]
        public async Task<ActionResult> Set([FromForm]TreeData models)
        {
            var result = await _presetFunctionServices.UpdateFunctionTree(models);
            return Json(result.Status.ToString());
        }
        public async Task<List<TreeDataModel>> GetReFunctions(int id)
        {
            var trees = await GenerateTree(id);
            return trees;
        }
        public async Task<List<TreeDataModel>> GenerateTree(int id)
        {
            var vm = await _companyServices.GetById(id);
            var reFunctions =await _presetFunctionServices.GetList();
            var functions =await _presetFunctionServices.GetFunctionList(id);
            List<TreeDataModel> trees = new List<TreeDataModel>();
            foreach (var reFunction in reFunctions)
            {
                var func =  functions.FirstOrDefault(o=>o.Code==reFunction.Code);
                if (func == null)
                {
                    #region 生成children
                    List<TreeDataModel> childrenTrees = new List<TreeDataModel>();
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName1))
                    {
                        childrenTrees.Add(new TreeDataModel() {id=reFunction.Code + ",OpName1", title = reFunction.OpName1 , spread = true });
                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName2))
                    {
                        childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName2", title = reFunction.OpName2, spread = true });
                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName3))
                    {
                        childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName3", title = reFunction.OpName3, spread = true });
                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName4))
                    {
                        childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName4", title = reFunction.OpName4, spread = true });
                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName5))
                    {
                        childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName5", title = reFunction.OpName5, spread = true });
                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName6))
                    {
                        childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName6", title = reFunction.OpName6, spread = true });
                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName7))
                    {
                        childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName7", title = reFunction.OpName7, spread = true });
                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName8))
                    {
                        childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName8", title = reFunction.OpName8, spread = true });
                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName9))
                    {
                        childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName9", title = reFunction.OpName9, spread = true });
                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName10))
                    {
                        childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName10", title = reFunction.OpName10, spread = true });
                    }
                    #endregion
                    trees.Add(new TreeDataModel()
                    {
                        id = reFunction.Code,
                        title = reFunction.Name,
                        children = childrenTrees.ToArray(),
                        spread = true
                    });
                }
                else
                {
                    #region 生成children
                    List<TreeDataModel> childrenTrees = new List<TreeDataModel>();
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName1))
                    {
                        if (reFunction.OpName1 == func.OpName1)
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName1", title = reFunction.OpName1, Checked = true, spread = true });
                        }
                        else
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName1", title = reFunction.OpName1, spread = true });
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName2))
                    {
                        if (reFunction.OpName2 == func.OpName2)
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName2", title = reFunction.OpName2, Checked = true, spread = true });
                        }
                        else
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName2", title = reFunction.OpName2, spread = true });
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName3))
                    {
                        if (reFunction.OpName3 == func.OpName3)
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName3", title = reFunction.OpName3, Checked = true, spread = true });
                        }
                        else
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName3", title = reFunction.OpName3, spread = true });
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName4))
                    {
                        if (reFunction.OpName4 == func.OpName4)
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName4", title = reFunction.OpName4, Checked = true, spread = true });
                        }
                        else
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName4", title = reFunction.OpName4, spread = true });
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName5))
                    {
                        if (reFunction.OpName5 == func.OpName5)
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName5", title = reFunction.OpName5, Checked = true, spread = true });
                        }
                        else
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName5", title = reFunction.OpName5, spread = true });
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName6))
                    {
                        if (reFunction.OpName6 == func.OpName6)
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName6", title = reFunction.OpName6, Checked = true, spread = true });
                        }
                        else
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName6", title = reFunction.OpName6, spread = true });
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName7))
                    {
                        if (reFunction.OpName7 == func.OpName7)
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName7", title = reFunction.OpName7, Checked = true, spread = true });
                        }
                        else
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName7", title = reFunction.OpName7, spread = true });
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName8))
                    {
                        if (reFunction.OpName8 == func.OpName8)
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName8", title = reFunction.OpName8, Checked = true, spread = true });
                        }
                        else
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName8", title = reFunction.OpName8, spread = true });
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName9))
                    {
                        if (reFunction.OpName9 == func.OpName9)
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName9", title = reFunction.OpName9, Checked = true, spread = true });
                        }
                        else
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName9", title = reFunction.OpName9, spread = true });
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(reFunction.OpName10))
                    {
                        if (reFunction.OpName10 == func.OpName10)
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName10", title = reFunction.OpName10, Checked = true, spread = true });
                        }
                        else
                        {
                            childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName10", title = reFunction.OpName10, spread = true });
                        }

                    }
                    #endregion
                    trees.Add(new TreeDataModel()
                    {
                        id = reFunction.Code,
                        title = reFunction.Name,
                        children = childrenTrees.ToArray(),
                        spread = true
                    });
                }
            }
            return trees;
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Set(int id, CompanyViewModel vm)
        {
            try
            {
                var result = await _companyServices.Update(vm);
                if (result.Status != Status.ok)
                {
                    return Content("<script>alert('保存失败');history.back();</script>");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View();
            }
        }

        public async Task<ActionResult> Setting(int id)
        {
            var vm = await _companyServices.GetSettingById(id);
            return View(vm);
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

        public async Task<ActionResult> DeleteIds(string ids)
        {
            try
            {
                var result = await _companyServices.Delete(ids);
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

        public async Task Init(CompanyViewModel vm)
        {
            vm.ComapnyStatuss = _basicInfoServices.GetCompanyStatus();
        }
    }
}