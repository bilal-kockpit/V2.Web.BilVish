using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace V2.Controllers
{
    public class HomeController : BaseController
    {
        private IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _baseConfig = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!ISVENDOR) return RedirectToAction("Logout", "Login");
            return View();
        }

        public IActionResult SuperAdmin()
        {
            if (!ISSUPERADMIN) return RedirectToAction("Logout", "Login");
            return View();
        }

        public IActionResult Admin()
        {
            if (!ISADMIN) return RedirectToAction("Logout", "Login");
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}

