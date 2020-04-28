namespace MDManagement.Web.Controllers
{
    using MDManagement.Data.Models;
    using MDManagement.Services;
    using MDManagement.Services.Data;
    using MDManagement.Services.Models.Company;
    using MDManagement.Services.Models.Employee;
    using MDManagement.Services.Models.JobTitle;
    using MDManagement.Services.Models.Town;
    using MDManagement.Web.Data;
    using MDManagement.Web.Data.Migrations;
    using MDManagement.Web.ViewModels.Management;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Collections.Generic;
    using System.Linq;

    public class ManagementController : Controller
    {
        private readonly ITownDataService townDataService;
        private readonly MDManagementDbContext data;
        private readonly IEmployeeDataService employeeService;
        private readonly ICompanyDataService comapnyService;
        private readonly UserManager<Employee> userManager;
        private readonly IJobTitleDataService jobTitleService;

        public ManagementController(ITownDataService townDataService,
                                    MDManagementDbContext data,
                                    IEmployeeDataService employeeService,
                                    ICompanyDataService comapnyService,
                                    UserManager<Employee> userManager,
                                    IJobTitleDataService jobTitleService)
        {
            this.townDataService = townDataService;
            this.data = data;
            this.employeeService = employeeService;
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
        public IActionResult CompanyCreation(CompanyCreationInputModel companyCreationInputModel)
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
                        var createJobTitleServiceModel = new CreateJobTitleServiceModel()
                        {
                            Name = companyCreationInputModel.BossTitle.ToString()
                        };

                        jobTitleService.CreateJobTitile(createJobTitleServiceModel);

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

                    return this.RedirectToAction("Index", "Home");
                }
               
            }
            return View(companyCreationInputModel);
        }


        //int companyId = comapnyService.Register(companyCreationInputModel.Name, companyCreationInputModel.Description);

        //employeeDataService.AddCompanyToEmployee(companyCreationInputModel.CurrentEmployeeId, companyId);

        //employeeDataService.AddJobTitleToEmployee(companyCreationInputModel.CurrentEmployeeId, companyCreationInputModel.BossTitle);

        //return this.RedirectToAction("Index","Home");




        //--------------------------------------------------------------------------
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
    }
}

