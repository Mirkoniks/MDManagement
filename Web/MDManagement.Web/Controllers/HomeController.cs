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
using MDManagement.Web.Data;
using MDManagement.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;

namespace MDManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Employee> userManager;
        private readonly MDManagementDbContext data;
        private readonly ICompanyDataService companyDataService;
        private readonly SignInManager<Employee> signInManager;

        public HomeController(ILogger<HomeController> logger,
            UserManager<Employee> userManager,
            MDManagementDbContext data,
            ICompanyDataService companyDataService,
            SignInManager<Employee> signInManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.data = data;
            this.companyDataService = companyDataService;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();

            if (signInManager.IsSignedIn(User))
            {
                var user = userManager.GetUserAsync(this.User);


                if (user != null)
                {
                    var companyId = user.Result.CompanyId;

                    if (companyId == null)
                    {
                        model.HasFrim = false;
                    }
                    else
                    {
                        model.HasFrim = true;
                        model.FirmName = companyDataService.FindById(companyId).Name;
                    }
                }

                return View(model);
            }
            model.FirmName = "";
            model.HasFrim = true;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HttpError(int statusCode)
        {

            return View(statusCode);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
