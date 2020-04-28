using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MDManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using MDManagement.Data.Models;
using MDManagement.Web.ViewModels.LoggedHome;
using MDManagement.Services.Data;

namespace MDManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Employee> userManager;

        public HomeController(ILogger<HomeController> logger,
            UserManager<Employee> userManager
            )
        {
            _logger = logger;
            this.userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
