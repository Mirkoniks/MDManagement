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

    public class ManagementController : Controller
    {
        private readonly ITownDataService townDataService;
        private readonly MDManagementDbContext data;
        private readonly IEmployeeDataService employeeService;
        private readonly ICompanyDataService comapnyService;
        private readonly UserManager<Employee> userManager;
        private readonly IJobTitleDataService jobTitleService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDepatmentDataService depatmentDataService;

        public ManagementController(ITownDataService townDataService,
                                    MDManagementDbContext data,
                                    IEmployeeDataService employeeService,
                                    ICompanyDataService comapnyService,
                                    UserManager<Employee> userManager,
                                    IJobTitleDataService jobTitleService,
                                    RoleManager<IdentityRole> roleManager,
                                    IDepatmentDataService depatmentDataService)
        {
            this.townDataService = townDataService;
            this.data = data;
            this.employeeService = employeeService;
            this.comapnyService = comapnyService;
            this.userManager = userManager;
            this.jobTitleService = jobTitleService;
            this.roleManager = roleManager;
            this.depatmentDataService = depatmentDataService;
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

                    employeeService.AddCompanyToEmployee(addCompanyToEmployeeServiceModel);

                    if (!jobTitleService.Exists(companyCreationInputModel.BossTitle))
                    {
                        jobTitleService.CreateJobTitile(companyCreationInputModel.BossTitle.ToString());

                        var companyId = jobTitleService.FindByName(companyCreationInputModel.BossTitle).Id;

                        var addJobTitleToEmployeServiceModel = new AddJobTitleToEmployeServiceModel()
                        {
                            EmployeeId = userManager.GetUserId(this.User),
                            JobTitleId = companyId
                        };

                        employeeService.AddJobTitleToEployee(addJobTitleToEmployeServiceModel);
                    }
                    else
                    {
                        var companyId = jobTitleService.FindByName(companyCreationInputModel.BossTitle).Id;

                        var addJobTitleToEmployeServiceModel = new AddJobTitleToEmployeServiceModel()
                        {
                            EmployeeId = userManager.GetUserId(this.User),
                            JobTitleId = companyId
                        };

                        employeeService.AddJobTitleToEployee(addJobTitleToEmployeServiceModel);
                    }


                    string roleName = "Manager";

                    bool roleExists = await roleManager.RoleExistsAsync(roleName);

                    if (!roleExists)
                    {
                        var role = new IdentityRole();
                        role.Name = roleName;
                        await roleManager.CreateAsync(role);
                    }

                    var employeeId = userManager.GetUserId(this.User);
                    
                    var user = await employeeService.FindById(employeeId);

                    await userManager.AddToRoleAsync(user, "Manager");

                    return this.RedirectToAction("Index", "Home");
                }


            }
            return View(companyCreationInputModel);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult AllEmployees()
        {
            var userCompanyId = userManager.GetUserAsync(this.User).Result.CompanyId;

            var allEmployeesViewModel = new AllEployeesViewModel()
            {
                Employees = employeeService.GetAllEmployees(userCompanyId).Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    Salary = e.Salary,
                    JobTitle = e.JobTitle,
                    Department = e.Department
                })
            };

            return View(allEmployeesViewModel);
        }

        public IActionResult CreateTown()
        {
            return View();
        }

        //Town createing

        [HttpPost]
        public IActionResult CreateTown(CreateTownInputModel createTownInputModel)
        {

            string name = createTownInputModel.Name;
            int postCode = createTownInputModel.PostCode;

            townDataService.Create(name, postCode);

            return View(createTownInputModel);
        }

        public IActionResult DeleteTown()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteTown(DeleteTownInputModel deleteTownInputModel)
        {
            var name = deleteTownInputModel.Name;

            townDataService.Delete(name);

            return View(deleteTownInputModel);
        }

        public IActionResult UpdateTown()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateTown(UpdateTownInputModel updateTownInputModel, int townId)
        {
            townDataService.Update(townId, updateTownInputModel.NewName, updateTownInputModel.NewPostCode);

            return View(updateTownInputModel);
        }

        public IActionResult AllTowns()
        {
            AllTownsViewModel allTownsViewModel = new AllTownsViewModel();

            var towns = townDataService.GetAllTowns();

            var toview = towns.Select(t => new TownViewModel { Name = t.Name, PostCode = t.PostCode });

            allTownsViewModel.Towns = toview;

            return View(allTownsViewModel);
        }

        public IActionResult EditUser(string userid)
        {
            var editUserViewModel = new EditUserViewModel()
            {
                UserId = userid
            };

            return View(editUserViewModel);
        }

        [HttpPost]
        public IActionResult EditUser(EditUserViewModel model)
        {
            var editUserServiceModel = new EditUserServiceModel() 
            { 
                Salary = model.Salary,
                HireDate = model.HireDate,
            };

            if (!jobTitleService.Exists(model.JobTitle))
            {
                jobTitleService.CreateJobTitile(model.JobTitle);

                editUserServiceModel.JobTitleId = jobTitleService.FindByName(model.JobTitle).Id;
            }
            else
            {
                editUserServiceModel.JobTitleId = jobTitleService.FindByName(model.JobTitle).Id;
            }


            if (!depatmentDataService.Exists(model.Department))
            {
                depatmentDataService.Create(model.Department);

                editUserServiceModel.DepartmentId = depatmentDataService.FindByName(model.Department).DepartmentId;
            }
            else
            {
                editUserServiceModel.DepartmentId = depatmentDataService.FindByName(model.Department).DepartmentId;
            }


            if (!employeeService.Exists(model.ManagerNickname))
            {
                ModelState.AddModelError("ManagerNickname", "Username is inavalid");

                return View(model);
            }
            else
            {
                editUserServiceModel.ManagerId = employeeService.FindByNickname(model.ManagerNickname);
            }

            editUserServiceModel.EmployeeId = userManager.GetUserId(this.User);

            employeeService.EditUserDetails(editUserServiceModel);

            return this.RedirectToAction("Index", "Home");
        }
    }
}

