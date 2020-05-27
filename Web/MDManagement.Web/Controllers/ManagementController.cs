namespace MDManagement.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using MDManagement.Data.Models;
    using MDManagement.Services.Data;
    using MDManagement.Services.Models.Company;
    using MDManagement.Services.Models.Employee;
    using MDManagement.Services.Models.JobTitle;
    using MDManagement.Web.Data;
    using MDManagement.Web.ViewModels.Management;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using MDManagement.Services;

    public class ManagementController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IEmployeeDataService employeeDataService;
        private readonly ICompanyDataService comapnyService;
        private readonly UserManager<Employee> userManager;
        private readonly IJobTitleService jobTitleService;

        public ManagementController(ITownDataService townDataService,
                                    MDManagementDbContext data,
                                    IEmployeeService employeeService,
                                    IEmployeeDataService employeeDataService,
                                    ICompanyDataService comapnyService,
                                    UserManager<Employee> userManager,
                                    IJobTitleService jobTitleService,
                                    IJobTitleDataService jobTitleDataService,
                                    RoleManager<IdentityRole> roleManager,
                                    IDepatmentDataService depatmentDataService,
                                    IAddressDataService addressDataService)
        {
            this.employeeService = employeeService;
            this.employeeDataService = employeeDataService;
            this.comapnyService = comapnyService;
            this.userManager = userManager;
            this.jobTitleService = jobTitleService;
        }

        [HttpGet]
        public IActionResult CompanyCreation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompanyCreationAsync(CompanyCreationInputModel companyCreationInputModel)
        {
            if (ModelState.IsValid)
            {
                if (comapnyService.Exists(companyCreationInputModel.Name))
                {
                    ModelState.AddModelError("Name", "Name already exists");

                    return View(companyCreationInputModel);
                }
                else
                {
                    var createCompanyServiceModel = new CreateCompanyServiceModel()
                    {
                        Name = companyCreationInputModel.Name,
                        Description = companyCreationInputModel.Description
                    };

                    comapnyService.Create(createCompanyServiceModel);


                    var addCompanyToEmployeeServiceModel = new AddCompanyToEmployeeServiceModel()
                    {
                        CompanyId = comapnyService.FindByName(companyCreationInputModel.Name).Id,
                        EmployeeId = userManager.GetUserId(this.User)
                    };

                    employeeDataService.AddCompanyToEmployee(addCompanyToEmployeeServiceModel);

                   
                    var thisUser = this.User;
                    jobTitleService.AddJobTitle(companyCreationInputModel.BossTitle, thisUser);


                    await employeeService.AddManagerRole(thisUser);


                    return this.RedirectToAction("Index", "Home");
                }


            }
            return View(companyCreationInputModel);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult AllEmployees()
        {
            var userCompanyId = userManager.GetUserAsync(this.User).Result.CompanyId;
            var userId = userManager.GetUserId(this.User);

            var allEmployeesViewModel = employeeService.GetAllEmployees(userCompanyId, userId);

            return View(allEmployeesViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserAsync(string employeeId)
        {
            var editUserViewModel = employeeService.EditUserAsync(employeeId).Result;

            return View(editUserViewModel);
        }

        [HttpPost]
        public IActionResult EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var editUserServiceModel = employeeService.EditUser(model);

                if (!employeeDataService.Exists(model.ManagerNickname))
                {
                    ModelState.AddModelError("ManagerNickname", "Username is inavalid");

                    return View(model);
                }
                else
                {
                    editUserServiceModel.ManagerId = employeeDataService.FindByNickname(model.ManagerNickname);
                }

                editUserServiceModel.EmployeeId = model.EmployeeId;

                employeeDataService.EditUserDetailsAsync(editUserServiceModel);

                return this.RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult UnconfirmedEmployees()
        {
            int? companyId = userManager.GetUserAsync(this.User).Result.CompanyId;

            var allUnconfirmedEmployeesViewModel = employeeService.UnconfirmedEmployees(companyId);

            return View(allUnconfirmedEmployeesViewModel);
        }

        [HttpPost]
        public IActionResult UnconfirmedEmployees(string employeeId)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConfirmEmployee(string employeeId)
        {


          var allUnconfirmedEmployeesViewModel = employeeService.ConfirmEmployee(employeeId);

            return View(allUnconfirmedEmployeesViewModel);
        }

        [HttpPost]
        public IActionResult ConfirmEmployee(ConfirmEmployeeViewModel model)
        {
            employeeDataService.ConfirmEmployee(model.EmployeeId);

            return RedirectToAction("UnconfirmedEmployees", "Management");
        }
    }
}

