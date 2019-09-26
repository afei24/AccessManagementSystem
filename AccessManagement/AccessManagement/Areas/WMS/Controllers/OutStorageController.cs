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
    public class OutStorageController : BaseController
    {
        private OutStorageServices _outStorageServices;
        private BasicInfoServices _basicInfoServices;
        public OutStorageController(OutStorageServices outStorageServices, BasicInfoServices basicInfoServices
            , ILogger<OutStorageController> logger)
            : base(logger)
        {
            _outStorageServices = outStorageServices;
            _basicInfoServices = basicInfoServices;
        }

        // GET: IMS/Location
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _outStorageServices.GetList(GetFilters(), GetSort(), GetAccount());
            return Json(result);
        }
        public OutStorageFilters GetFilters()
        {
            var filters = new OutStorageFilters()
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
            var vm = new OutStorageViewModel();
            await Init(vm);
            List<OutStoDetailViewModel> outStorDetails = new List<OutStoDetailViewModel>();
            HttpContext.Session.Set("OutStorDetail", SerializeHelper.SerializeToBinary(outStorDetails));
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OutStorageViewModel vm)
        {
            vm.OrderNum = Guid.NewGuid().ToString("N");
            List<OutStoDetailViewModel> outStorDetails  = new List<OutStoDetailViewModel>();
            byte[] outStorDetailsByte = null;
            if (HttpContext.Session.TryGetValue("OutStorDetail", out outStorDetailsByte))
            {
                outStorDetails = (List<OutStoDetailViewModel>)SerializeHelper.DeserializeWithBinary(outStorDetailsByte);
            }
            var result = await _outStorageServices.Create(vm, outStorDetails, GetAccount());

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
            var vm = await _outStorageServices.GetById((int)id);
            var inStorDetails = await _outStorageServices.GetDetailByOrderNum(vm.OrderNum);
            HttpContext.Session.Set("OutStorDetail", SerializeHelper.SerializeToBinary(inStorDetails));
            await Init(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OutStorageViewModel vm)
        {
            List<OutStoDetailViewModel> outStorDetails = new List<OutStoDetailViewModel>();
            byte[] outStorDetailsByte = null;
            if (HttpContext.Session.TryGetValue("OutStorDetail", out outStorDetailsByte))
            {
                outStorDetails = (List<OutStoDetailViewModel>)SerializeHelper.DeserializeWithBinary(outStorDetailsByte);
            }
            var result = await _outStorageServices.Update(vm, outStorDetails, GetAccount());
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


        public async Task<ActionResult> DeleteIds(string ids)
        {
            try
            {
                var result = await _outStorageServices.Delete(ids);
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
                var result = await _outStorageServices.Check(id, GetAccount());
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

        public async Task Init(OutStorageViewModel vm)
        {
            vm.Customers = await _basicInfoServices.GetCustomers(GetAccount());
            vm.Customers.Insert(0, new SelectListItem() { Value = "0", Text = "" });
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
                addPrductHtml += "<option value='" + product.Value + "'>" + product.Text + "</option>";
            }
            addPrductHtml += @"
                    </select>
                </div>
            </div>";
            return Json(locationHtml + addPrductHtml);
        }

        [HttpPost]
        public async Task<ActionResult> AddStorDetail(OutStoDetailViewModel vm)
        {
            try
            {
                var localProduct = await _basicInfoServices.GetLocalProducts(GetAccount(),vm.ProductNum,vm.LocalNum);
                if (localProduct == null || localProduct.Num < vm.Num)
                {
                    return Json("库存不够！");
                }
                var proId = Convert.ToInt32(vm.ProductNum);
                var product = (await _basicInfoServices.GetProduct(GetAccount())).FirstOrDefault(o => o.Id == proId);
                vm.ProductName = product.ProductName;
                vm.BarCode = product.BarCode;
                List<OutStoDetailViewModel> outStorDetails = new List<OutStoDetailViewModel>();
                byte[] outStorDetailsByte = null;
                if (HttpContext.Session.TryGetValue("OutStorDetail", out outStorDetailsByte))
                {
                    outStorDetails = (List<OutStoDetailViewModel>)SerializeHelper.DeserializeWithBinary(outStorDetailsByte);
                    var outStorDetail = outStorDetails.FirstOrDefault(o => o.ProductNum == vm.ProductNum && o.LocalNum == vm.LocalNum);
                    if (outStorDetail == null)
                    {
                        outStorDetails.Add(vm);
                    }
                    else
                    {
                        outStorDetail.Num += vm.Num;
                        outStorDetail.OutPrice = vm.OutPrice;
                    }
                }
                else
                {
                    outStorDetails.Add(vm);
                }

                HttpContext.Session.Set("OutStorDetail", SerializeHelper.SerializeToBinary(outStorDetails));
                return Json(outStorDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        public ActionResult GetStorDetail()
        {
            try
            {
                List<OutStoDetailViewModel> outStorDetails = new List<OutStoDetailViewModel>();
                byte[] outStorDetailsByte = null;
                if (HttpContext.Session.TryGetValue("OutStorDetail", out outStorDetailsByte))
                {
                    outStorDetails = (List<OutStoDetailViewModel>)SerializeHelper.DeserializeWithBinary(outStorDetailsByte);
                }
                ResponseModel<OutStoDetailViewModel> result = new ResponseModel<OutStoDetailViewModel>();
                result.status = 0;
                result.message = "";
                result.total = outStorDetails.Count();
                result.data = outStorDetails;
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult DeleteStorDetail(string productNum, string localNum)
        {
            try
            {
                List<OutStoDetailViewModel> outStorDetails = new List<OutStoDetailViewModel>();
                byte[] outStorDetailsByte = null;
                if (HttpContext.Session.TryGetValue("OutStorDetail", out outStorDetailsByte))
                {
                    outStorDetails = (List<OutStoDetailViewModel>)SerializeHelper.DeserializeWithBinary(outStorDetailsByte);
                    var outStorDetail = outStorDetails.FirstOrDefault(o => o.ProductNum == productNum && o.LocalNum == localNum);
                    outStorDetails.Remove(outStorDetail);
                }

                HttpContext.Session.Set("OutStorDetail", SerializeHelper.SerializeToBinary(outStorDetails));
                return Json(outStorDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditStorDetail(OutStoDetailViewModel vm)
        {
            try
            {
                var localProduct = await _basicInfoServices.GetLocalProducts(GetAccount(), vm.ProductNum, vm.LocalNum);
                if (localProduct == null || localProduct.Num < vm.Num)
                {
                    return Json("库存不够！");
                }
                var proId = Convert.ToInt32(vm.ProductNum);
                var product = (await _basicInfoServices.GetProduct(GetAccount())).FirstOrDefault(o => o.Id == proId);
                vm.ProductName = product.ProductName;
                vm.BarCode = product.BarCode;
                List<OutStoDetailViewModel> outStorDetails = new List<OutStoDetailViewModel>();
                byte[] outStorDetailsByte = null;
                if (HttpContext.Session.TryGetValue("OutStorDetail", out outStorDetailsByte))
                {
                    outStorDetails = (List<OutStoDetailViewModel>)SerializeHelper.DeserializeWithBinary(outStorDetailsByte);
                    var outStorDetail = outStorDetails.FirstOrDefault(o => o.ProductNum == vm.ProductNum && o.LocalNum == vm.LocalNum);
                    if (outStorDetail == null)
                    {
                        outStorDetails.Add(vm);
                    }
                    else
                    {
                        outStorDetail.Num = vm.Num;
                        outStorDetail.OutPrice = vm.OutPrice;
                    }
                }
                else
                {
                    outStorDetails.Add(vm);
                }

                HttpContext.Session.Set("OutStorDetail", SerializeHelper.SerializeToBinary(outStorDetails));
                return Json(outStorDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}