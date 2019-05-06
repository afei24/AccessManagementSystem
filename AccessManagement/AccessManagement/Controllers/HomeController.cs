using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AccessManagement.Models;
using Microsoft.Extensions.Logging;

namespace AccessManagement.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger)
            :base(logger)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(object o)
        {
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
