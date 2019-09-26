using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Controllers;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS.WMS.WWMS;
using AccessManagementServices.Filters;
using AccessManagementServices.Models;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AccessManagement.Areas.WMS.Controllers
{
    [Area("WMS")]
    public class InStorageController : BaseController
    {
        private InStorageServices _inStorageServices;
        private BasicInfoServices _basicInfoServices;
        public InStorageController(InStorageServices inStorageServices, BasicInfoServices basicInfoServices
            , ILogger<InStorageController> logger)
            : base(logger)
        {
            _inStorageServices = inStorageServices;
            _basicInfoServices = basicInfoServices;
        }

        // GET: IMS/Location
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _inStorageServices.GetList(GetFilters(), GetSort(),GetAccount());
            return Json(result);
        }
        public InStorageFilters GetFilters()
        {
            var filters = new InStorageFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                OrderNum = HttpContext.Request.Query["orderNum"],
                Code = HttpContext.Request.Query["code"],
                Status = HttpContext.Request.Query["status"],
                StartDateTime = HttpContext.Request.Query["startTime"],
                EndDateTime = HttpContext.Request.Query["endTime"],
            };
            return filters;
        }

        // GET: InStorage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var vm = new InStorageViewModel();
            await Init(vm);
            List<InStorDetailViewModel> inStorDetails = new List<InStorDetailViewModel>();
            HttpContext.Session.Set("InStorDetail", SerializeHelper.SerializeToBinary(inStorDetails));
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InStorageViewModel vm)
        {
            vm.OrderNum = Guid.NewGuid().ToString("N");
            List<InStorDetailViewModel> inStorDetails = new List<InStorDetailViewModel>();
            byte[] inStorDetailsByte = null;
            if (HttpContext.Session.TryGetValue("InStorDetail", out inStorDetailsByte))
            {
                inStorDetails = (List<InStorDetailViewModel>)SerializeHelper.DeserializeWithBinary(inStorDetailsByte);
            }
            var result = await _inStorageServices.Create(vm, inStorDetails, GetAccount());
            
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

        public async Task<IActionResult> Edit(int? id)
        {
            var vm = await _inStorageServices.GetById((int)id);
            var inStorDetails = await _inStorageServices.GetDetailByOrderNum(vm.OrderNum);
            HttpContext.Session.Set("InStorDetail", SerializeHelper.SerializeToBinary(inStorDetails));
            await Init(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InStorageViewModel vm)
        {
            List<InStorDetailViewModel> inStorDetails = new List<InStorDetailViewModel>();
            byte[] inStorDetailsByte = null;
            if (HttpContext.Session.TryGetValue("InStorDetail", out inStorDetailsByte))
            {
                inStorDetails = (List<InStorDetailViewModel>)SerializeHelper.DeserializeWithBinary(inStorDetailsByte);
            }
            var result = await _inStorageServices.Update(vm, inStorDetails, GetAccount());
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

        // GET: InStorage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InStorage/Delete/5
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
                var result = await _inStorageServices.Delete(ids);
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

        public async Task<ActionResult> Check(int id)
        {
            try
            {
                var result = await _inStorageServices.Check(id,GetAccount());
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

        public async Task Init(InStorageViewModel vm)
        {
            vm.Suppliers = await _basicInfoServices.GetSuppliers(GetAccount());
            vm.Suppliers.Insert(0,new SelectListItem() { Value = "0",Text=""});
            //vm.Goods = await _basicInfoServices.GetGoods(GetAccount());
            //vm.Customers = await _basicInfoServices.GetCustomers(GetAccount());
            //vm.Measures = await _basicInfoServices.GetMeasures(GetAccount());
        }

        public async Task<ActionResult> GetAddProductHtml()
        {
            var locations = await _basicInfoServices.GetLocations(GetAccount());
            string locationHtml = @"<div class='layui-input-inline' style='width: 250px;'>
            <label class='layui-label' style='width: 100px;'>库位</label>
            <div class='layui-input-inline'>
                <select id='location' style='width: 200px;' class='layui-select' lay-verify='required'  lay-search><option value=''></option>";
            foreach (var location in locations)
            {
                locationHtml += "<option value='" + location.Text + "'>" + location.Text + "</option>";
            }
            locationHtml += @"
                    </select>
                </div>
            </div>";
            var products = await _basicInfoServices.GetProducts(GetAccount());
            string addPrductHtml = @"<div class='layui-input-inline' style='width: 250px;'>
            <label class='layui-label' style='width: 100px;'>产品</label>
            <div class='layui-input-inline'>
                <select id='product' style='width: 200px;' class='layui-select' lay-verify='required'  lay-search><option value=''></option>";
            foreach (var product in products)
            {
                addPrductHtml += "<option value='"+ product.Value+ "'>" + product.Text + "</option>";
            }
            addPrductHtml += @"
                    </select>
                </div>
            </div>";
            return Json(locationHtml+addPrductHtml);
        }

        [HttpPost]
        public async Task<ActionResult> AddInStorDetail(InStorDetailViewModel vm)
        {
            try
            {
                var proId = Convert.ToInt32(vm.ProductNum);
                var product = (await _basicInfoServices.GetProduct(GetAccount())).FirstOrDefault(o=>o.Id == proId);
                vm.ProductName = product.ProductName;
                vm.BarCode = product.BarCode;
                List<InStorDetailViewModel> inStorDetails = new List<InStorDetailViewModel>();
                byte[] inStorDetailsByte = null;
                if (HttpContext.Session.TryGetValue("InStorDetail", out inStorDetailsByte))
                {
                    inStorDetails = (List<InStorDetailViewModel>)SerializeHelper.DeserializeWithBinary(inStorDetailsByte);
                    var inStorDetail = inStorDetails.FirstOrDefault(o=>o.ProductNum == vm.ProductNum && o.LocalNum == vm.LocalNum);
                    if (inStorDetail == null)
                    {
                        inStorDetails.Add(vm);
                    }
                    else
                    {
                        inStorDetail.Num += vm.Num;
                        inStorDetail.InPrice = vm.InPrice;
                    }
                }
                else
                {
                    inStorDetails.Add(vm);
                }
                
                HttpContext.Session.Set("InStorDetail", SerializeHelper.SerializeToBinary(inStorDetails));
                return Json(inStorDetails);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        public ActionResult GetInStorDetail()
        {
            try
            {
                List<InStorDetailViewModel> inStorDetails = new List<InStorDetailViewModel>();
                byte[] inStorDetailsByte = null;
                if (HttpContext.Session.TryGetValue("InStorDetail", out inStorDetailsByte))
                {
                    inStorDetails = (List<InStorDetailViewModel>)SerializeHelper.DeserializeWithBinary(inStorDetailsByte);
                }
                ResponseModel<InStorDetailViewModel> result = new ResponseModel<InStorDetailViewModel>();
                result.status = 0;
                result.message = "";
                result.total = inStorDetails.Count();
                result.data = inStorDetails;
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult DeleteInStorDetail(string productNum,string localNum)
        {
            try
            {
                List<InStorDetailViewModel> inStorDetails = new List<InStorDetailViewModel>();
                byte[] inStorDetailsByte = null;
                if (HttpContext.Session.TryGetValue("InStorDetail", out inStorDetailsByte))
                {
                    inStorDetails = (List<InStorDetailViewModel>)SerializeHelper.DeserializeWithBinary(inStorDetailsByte);
                    var inStorDetail = inStorDetails.FirstOrDefault(o => o.ProductNum == productNum && o.LocalNum == localNum);
                    inStorDetails.Remove(inStorDetail);
                }

                HttpContext.Session.Set("InStorDetail", SerializeHelper.SerializeToBinary(inStorDetails));
                return Json(inStorDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditInStorDetail(InStorDetailViewModel vm)
        {
            try
            {
                var proId = Convert.ToInt32(vm.ProductNum);
                var product = (await _basicInfoServices.GetProduct(GetAccount())).FirstOrDefault(o => o.Id == proId);
                vm.ProductName = product.ProductName;
                vm.BarCode = product.BarCode;
                List<InStorDetailViewModel> inStorDetails = new List<InStorDetailViewModel>();
                byte[] inStorDetailsByte = null;
                if (HttpContext.Session.TryGetValue("InStorDetail", out inStorDetailsByte))
                {
                    inStorDetails = (List<InStorDetailViewModel>)SerializeHelper.DeserializeWithBinary(inStorDetailsByte);
                    var inStorDetail = inStorDetails.FirstOrDefault(o => o.ProductNum == vm.ProductNum && o.LocalNum == vm.LocalNum);
                    if (inStorDetail == null)
                    {
                        inStorDetails.Add(vm);
                    }
                    else
                    {
                        inStorDetail.Num = vm.Num;
                        inStorDetail.InPrice = vm.InPrice;
                    }
                }
                else
                {
                    inStorDetails.Add(vm);
                }

                HttpContext.Session.Set("InStorDetail", SerializeHelper.SerializeToBinary(inStorDetails));
                return Json(inStorDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}