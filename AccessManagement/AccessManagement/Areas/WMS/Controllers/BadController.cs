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
using Microsoft.Extensions.Logging;

namespace AccessManagement.Areas.WMS.Controllers
{
    [Area("WMS")]
    public class BadController : BaseController
    {
        private BadReportServices _badReportServices;
        private BasicInfoServices _basicInfoServices;
        public BadController(BadReportServices badReportServices, BasicInfoServices basicInfoServices
            , ILogger<BadController> logger)
            : base(logger)
        {
            _badReportServices = badReportServices;
            _basicInfoServices = basicInfoServices;
        }

        // GET: IMS/Location
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _badReportServices.GetList(GetFilters(), GetSort(),GetAccount());
            return Json(result);
        }
        public BadReportFilters GetFilters()
        {
            var filters = new BadReportFilters()
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

        public async Task<IActionResult> Create()
        {
            var vm = new BadReportViewModel();
 
            List<BadReportDetailViewModel> storDetails = new List<BadReportDetailViewModel>();
            HttpContext.Session.Set("StorDetail", SerializeHelper.SerializeToBinary(storDetails));
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BadReportViewModel vm)
        {
            vm.OrderNum = Guid.NewGuid().ToString("N");
            List<BadReportDetailViewModel> storDetails = new List<BadReportDetailViewModel>();
            byte[] storDetailsByte = null;
            if (HttpContext.Session.TryGetValue("StorDetail", out storDetailsByte))
            {
                storDetails = (List<BadReportDetailViewModel>)SerializeHelper.DeserializeWithBinary(storDetailsByte);
            }
            var result = await _badReportServices.Create(vm, storDetails, GetAccount());

            if (result.Status == Status.ok)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "保存失败: " + result.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var vm = await _badReportServices.GetById((int)id);
            var storDetails = await _badReportServices.GetDetailByOrderNum(vm.OrderNum);
            HttpContext.Session.Set("StorDetail", SerializeHelper.SerializeToBinary(storDetails));
            ViewBag.Starus = vm.Status;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BadReportViewModel vm)
        {
            List<BadReportDetailViewModel> storDetails = new List<BadReportDetailViewModel>();
            byte[] storDetailsByte = null;
            if (HttpContext.Session.TryGetValue("StorDetail", out storDetailsByte))
            {
                storDetails = (List<BadReportDetailViewModel>)SerializeHelper.DeserializeWithBinary(storDetailsByte);
            }
            var result = await _badReportServices.Update(vm, storDetails, GetAccount());
            if (result.Status == Status.ok)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "保存失败: " + result.Message);
                return View(vm);
            }
        }


        public async Task<ActionResult> DeleteIds(string ids)
        {
            try
            {
                var result = await _badReportServices.Delete(ids);
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

        public async Task<ActionResult> Check(int id, int badStatus)
        {
            try
            {
                var result = await _badReportServices.Check(id, badStatus, GetAccount());
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
        public async Task<ActionResult> AddStorDetail(BadReportDetailViewModel vm)
        {
            try
            {
                var localProduct = await _basicInfoServices.GetLocalProducts(GetAccount(), vm.ProductNum, vm.FromLocalNum);
                if (localProduct == null || localProduct.Num < vm.Num)
                {
                    return Json("库存不够！");
                }
                var proId = Convert.ToInt32(vm.ProductNum);
                var product = (await _basicInfoServices.GetProduct(GetAccount())).FirstOrDefault(o => o.Id == proId);
                vm.ProductName = product.ProductName;
                vm.BarCode = product.BarCode;
                List<BadReportDetailViewModel> storDetails = new List<BadReportDetailViewModel>();
                byte[] storDetailsByte = null;
                if (HttpContext.Session.TryGetValue("StorDetail", out storDetailsByte))
                {
                    storDetails = (List<BadReportDetailViewModel>)SerializeHelper.DeserializeWithBinary(storDetailsByte);
                    var outStorDetail = storDetails.FirstOrDefault(o => o.ProductNum == vm.ProductNum && o.FromLocalNum == vm.FromLocalNum);
                    if (outStorDetail == null)
                    {
                        storDetails.Add(vm);
                    }
                    else
                    {
                        outStorDetail.Num += vm.Num;
                    }
                }
                else
                {
                    storDetails.Add(vm);
                }

                HttpContext.Session.Set("StorDetail", SerializeHelper.SerializeToBinary(storDetails));
                return Json(storDetails);
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
                List<BadReportDetailViewModel> storDetails = new List<BadReportDetailViewModel>();
                byte[] storDetailsByte = null;
                if (HttpContext.Session.TryGetValue("StorDetail", out storDetailsByte))
                {
                    storDetails = (List<BadReportDetailViewModel>)SerializeHelper.DeserializeWithBinary(storDetailsByte);
                }
                ResponseModel<BadReportDetailViewModel> result = new ResponseModel<BadReportDetailViewModel>();
                result.status = 0;
                result.message = "";
                result.total = storDetails.Count();
                result.data = storDetails;
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
                List<BadReportDetailViewModel> storDetails = new List<BadReportDetailViewModel>();
                byte[] storDetailsByte = null;
                if (HttpContext.Session.TryGetValue("StorDetail", out storDetailsByte))
                {
                    storDetails = (List<BadReportDetailViewModel>)SerializeHelper.DeserializeWithBinary(storDetailsByte);
                    var storDetail = storDetails.FirstOrDefault(o => o.ProductNum == productNum && o.FromLocalNum == localNum);
                    storDetails.Remove(storDetail);
                }

                HttpContext.Session.Set("StorDetail", SerializeHelper.SerializeToBinary(storDetails));
                return Json(storDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditStorDetail(BadReportDetailViewModel vm)
        {
            try
            {
                var localProduct = await _basicInfoServices.GetLocalProducts(GetAccount(), vm.ProductNum, vm.FromLocalNum);
                if (localProduct == null || localProduct.Num < vm.Num)
                {
                    return Json("库存不够！");
                }
                var proId = Convert.ToInt32(vm.ProductNum);
                var product = (await _basicInfoServices.GetProduct(GetAccount())).FirstOrDefault(o => o.Id == proId);
                vm.ProductName = product.ProductName;
                vm.BarCode = product.BarCode;
                List<BadReportDetailViewModel> storDetails = new List<BadReportDetailViewModel>();
                byte[] storDetailsByte = null;
                if (HttpContext.Session.TryGetValue("StorDetail", out storDetailsByte))
                {
                    storDetails = (List<BadReportDetailViewModel>)SerializeHelper.DeserializeWithBinary(storDetailsByte);
                    var storDetail = storDetails.FirstOrDefault(o => o.ProductNum == vm.ProductNum && o.FromLocalNum == vm.FromLocalNum);
                    if (storDetail == null)
                    {
                        storDetails.Add(vm);
                    }
                    else
                    {
                        storDetail.Num = vm.Num;
                    }
                }
                else
                {
                    storDetails.Add(vm);
                }

                HttpContext.Session.Set("StorDetail", SerializeHelper.SerializeToBinary(storDetails));
                return Json(storDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}