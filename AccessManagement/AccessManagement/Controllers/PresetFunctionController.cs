using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccessManagement.Controllers
{
    public class PresetFunctionController : Controller
    {
        private PresetFunctionServices _presetFunctionServices;
        public PresetFunctionController(PresetFunctionServices presetFunctionServices)
        {
            _presetFunctionServices = presetFunctionServices;
        }
        // GET: PresetFunction
        public async Task<ActionResult> Index()
        {
            var vms = await _presetFunctionServices.GetList();
            return View(vms);
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var vms = await _presetFunctionServices.GetList();
            var json =  JsonConvert.SerializeObject(vms);
            return Json(json);
        }

        // GET: PresetFunction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PresetFunction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PresetFunction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PresetFunctionViewModel collection)
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

        // GET: PresetFunction/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var vm = await _presetFunctionServices.GetById(id);
            return View(vm);
        }

        // POST: PresetFunction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PresetFunctionViewModel vm)
        {
            try
            {
                var result =  await _presetFunctionServices.Update(vm);
                if (result.Status != Status.ok)
                {
                    return Content($"<script>alert('保存失败：{result.Message}');history.back();</script>");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PresetFunction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PresetFunction/Delete/5
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