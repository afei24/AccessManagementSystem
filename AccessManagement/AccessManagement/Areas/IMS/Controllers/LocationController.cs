using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Controllers;
using AccessManagementServices.Filters;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WMSData;

namespace AccessManagement.Areas.IMS.Controllers
{
    [Area("IMS")]
    public class LocationController : BaseController
    {
        private LocationServices _locationServices;
        private BasicInfoServices _basicInfoServices;
        public LocationController(LocationServices locationServices, BasicInfoServices basicInfoServices
            , ILogger<LocationController> logger)
            : base(logger)
        {
            _locationServices = locationServices;
            _basicInfoServices = basicInfoServices;
        }

        // GET: IMS/Location
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> AjaxIndex()
        {
            var result = await _locationServices.GetList(GetFilters(), GetSort());
            return Json(result);
        }
        public LocationFilters GetFilters()
        {
            var filters = new LocationFilters()
            {
                Page = Convert.ToInt32(HttpContext.Request.Query["page"]),
                Limit = Convert.ToInt32(HttpContext.Request.Query["limit"]),
                Name = HttpContext.Request.Query["name"],
                Code = HttpContext.Request.Query["code"],
            };
            return filters;
        }

        // GET: IMS/Location/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }

        // GET: IMS/Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IMS/Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LocalNum,LocalBarCode,LocalName,StorageNum,StorageType,LocalType,Rack,Length,Width,Height,X,Y,Z,UnitNum,UnitName,Remark,IsForbid,IsDefault,IsDelete,CreateTime")] Location location)
        {
            if (ModelState.IsValid)
            {
            }
            return View(location);
        }

        // GET: IMS/Location/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            return View();
        }

        // POST: IMS/Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LocalNum,LocalBarCode,LocalName,StorageNum,StorageType,LocalType,Rack,Length,Width,Height,X,Y,Z,UnitNum,UnitName,Remark,IsForbid,IsDefault,IsDelete,CreateTime")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(location);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: IMS/Location/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            return View();
        }

        // POST: IMS/Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
        }

    }
}
