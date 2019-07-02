using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AccessManagement.Models;
using Microsoft.Extensions.Logging;
using AccessManagementData;
using AccessManagementServices.DOTS;
using System.Net;
using System.Text;
using System.IO;
using AccessManagementServices.Common;

namespace AccessManagement.Controllers
{
    public class HomeController : BaseController
    {
        private readonly AccessManagementContext _context;

        public HomeController(ILogger<HomeController> logger, AccessManagementContext context)
            :base(logger)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(AccountViewModel vm)
        {
            HttpContext.Session.Set("account", SerializeHelper.SerializeToBinary(vm));
            return Redirect("Privacy");
        }

        public IActionResult Privacy()
        {
            var ex = new Exception("error");

            _logger.LogError(ex, ex.Message);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
