namespace MDManagement.Web.Controllers
{
    using MDManagement.Data.Models;
    using MDManagement.Services.Data;
    using MDManagement.Services.Models.Employee;
    using MDManagement.Web.ViewModels.LoggedHome;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class LoggedHomeController : Controller
    {
        private readonly UserManager<Employee> userManager;
        private readonly IEmployeeDataService employeeService;
        private readonly ICompanyDataService companyService;

        public LoggedHomeController(UserManager<Employee> userManager,
                                    IEmployeeDataService employeeService,
                                    ICompanyDataService companyService)
        {
            this.userManager = userManager;
            this.employeeService = employeeService;
            this.companyService = companyService;
        }

        public IActionResult Index()
        {
            var user = userManager.GetUserAsync(this.User);

            IndexViewModel indexViewModel = new IndexViewModel();

            int? companyId;

            if (user != null)
            {
                companyId = user.Result.CompanyId;

                if (companyId == null)
                {
                    indexViewModel.HasFirm = false;
                }
                else
                {
                    indexViewModel.HasFirm = true;
                }
            }



            return View(indexViewModel);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel indexInputModel)
        {
            if (ModelState.IsValid)
            {
                var userId = userManager.GetUserId(this.User);

                var companyCode = indexInputModel.CompanyCode;

                if (!companyService.IsValidCompany(companyCode))
                {
                    ModelState.AddModelError("CompanyCode", "Company code is not valid");

                    return View(indexInputModel);
                }
                else
                {
                    var companyId = companyService.FindByCompanyCode(companyCode).Id;

                    var addCompanyToEmployeeServiceModel = new AddCompanyToEmployeeServiceModel()
                    {
                        EmployeeId = userId,
                        CompanyId = companyId
                    };

                    employeeService.AddCompanyToEmployee(addCompanyToEmployeeServiceModel);
                }

                return RedirectToAction("Index");
            }
            return View(indexInputModel);
        }
    }
}
