namespace MDManagement.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MDManagement.Web.ViewModels.Project;
    using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
    using MDManagement.Services.Data;
    using Microsoft.AspNetCore.Identity;
    using MDManagement.Data.Models;
    using System.Linq;
    using MDManagement.Services.Models.Project;
    using MDManagement.Web.ViewModels.Management;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectDataService projectDataService;
        private readonly UserManager<Employee> userManager;
        private readonly IEmployeeDataService employeeDataService;
        private readonly IJobTitleDataService jobTitleDataService;
        private readonly IDepatmentDataService depatmentDataService;

        public ProjectController(IProjectDataService projectDataService,
                                 UserManager<Employee> userManager,
                                 IEmployeeDataService employeeDataService,
                                 IJobTitleDataService jobTitleDataService,
                                 IDepatmentDataService depatmentDataService)
        {
            this.projectDataService = projectDataService;
            this.userManager = userManager;
            this.employeeDataService = employeeDataService;
            this.jobTitleDataService = jobTitleDataService;
            this.depatmentDataService = depatmentDataService;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult AllProjects()
        {
            var projects = projectDataService.GetAllProjects(userManager.GetUserAsync(this.User).Result.CompanyId);

            var model = new AllProjectsViewModel()
            {
                AllProjects = projects.Select(p => new ProjectViewModel
                {
                    Id = projectDataService.FindProjectById(p.ProjectId).Id,
                    Name = projectDataService.FindProjectById(p.ProjectId).Name,
                    Description = projectDataService.FindProjectById(p.ProjectId).Description,
                    StartDate = projectDataService.FindProjectById(p.ProjectId).StartDate,
                    EndDate = projectDataService.FindProjectById(p.ProjectId).EndDate
                })
            };

            return View(model);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IActionResult Create(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!employeeDataService.Exists(model.ProjectManagerNick))
                {
                    ModelState.AddModelError("ProjectManagerNick", "Project Manager Nick is not valid");

                    return View(model);
                }

                var companyId = userManager.GetUserAsync(this.User).Result.CompanyId;


                var project = new ProjectServiceModel
                {
                    Name = model.Name,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    CompanyId = companyId,
                    ProjectManager = employeeDataService.FindByNickname(model.ProjectManagerNick)
                };

                projectDataService.Create(project);

                return RedirectToAction("AllProjects", "Project");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Join()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Join(JoinViewModel model) 
        {
            if (ModelState.IsValid)
            {
                if (!projectDataService.Exists(model.ProjectCode))
                {
                    ModelState.AddModelError("ProjectCode", "Project code is not valid");

                    return View(model);
                }

                var employeeId = userManager.GetUserId(this.User);

                var serviceModel = new JoinServiceModel
                {
                    EmployeeId = employeeId,
                    ProjectCode = model.ProjectCode
                };

                projectDataService.Join(serviceModel);

                return View();
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Info(int projectId)
        {
            var employees = projectDataService.GetAllEmployeesInProject(projectId)
                            .Select(e => new EmployeeViewModel
                            {
                                EmployeeId = e,
                                FirstName = employeeDataService.FindById(e).Result.FirstName,
                                MiddleName = employeeDataService.FindById(e).Result.MiddleName,
                                LastName = employeeDataService.FindById(e).Result.LastName,
                                Salary = employeeDataService.FindById(e).Result.Salary,
                                JobTitle = jobTitleDataService.FindById(employeeDataService.FindById(e).Result.JobTitleId).Name,
                                //Department = depatmentDataService.FindById(depatmentDataService.FindById(e).DepartmentId).DepartmentName,
                            });

            var model = new InfoViewModel
            {
                EmployeesInProject = employees,
                Description = projectDataService.FindProjectById(projectId).Description,
                Name = projectDataService.FindProjectById(projectId).Name,
                StartDate = projectDataService.FindProjectById(projectId).StartDate,
                EndDate = projectDataService.FindProjectById(projectId).EndDate
            };

            return View(model);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult Assign(string employeeId)
        {
            if (ModelState.IsValid)
            {
                var projects = projectDataService.GetAllProjects(userManager.GetUserAsync(this.User).Result.CompanyId);

                var model = new AssignViewModel()
                {
                    EmployeeId = employeeId,
                    AllProjects = projects.Select(p => new ProjectViewModel
                    {
                        Id = projectDataService.FindProjectById(p.ProjectId).Id,
                        Name = projectDataService.FindProjectById(p.ProjectId).Name,
                        Description = projectDataService.FindProjectById(p.ProjectId).Description,
                        StartDate = projectDataService.FindProjectById(p.ProjectId).StartDate,
                        EndDate = projectDataService.FindProjectById(p.ProjectId).EndDate
                    })
                };

                return View(model);
            }

            var model1 = new EmployeeProjectModel { employeeId = employeeId };

            return RedirectToAction("Assign", "Project", model1);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult AssignTo(int projectId, string employeeId)
        {
            if (ModelState.IsValid)
            {
                if (projectDataService.IsEmployeeInProject(employeeId, projectId))
                {
                    ModelState.AddModelError("EmployeeId", "This employee is already in this project");

                    var model = new EmployeeProjectModel { employeeId = employeeId };

                    return RedirectToAction("Assign", "Project", model);
                }

                projectDataService.Assign(employeeId, projectId);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Assign", "Project");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int projectId)
        {
            var project = projectDataService.FindProjectById(projectId);

            var model = new EditViewModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                IsCompleated = project.IsCompleated
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult Edit(EditViewModel model)
        {
            var serviceModel = new ProjectServiceModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                IsCompleated = model.IsCompleated
            };

            projectDataService.Edit(serviceModel);

            return RedirectToAction("AllProjects", "Project");
        }

        public IActionResult MyProjects()
        {
            var employeeId = userManager.GetUserId(this.User);

            var projectsId = projectDataService.GetEmployeeProjects(employeeId);

            var projects = new List<ProjectViewModel>();

            foreach (var projectId in projectsId)
            {
                var project = projectDataService.FindProjectById(projectId.ProjectId);

                var employees = projectDataService.GetAllEmployeesInProject(projectId.ProjectId)
                            .Select(e => new EmployeeViewModel
                            {
                                EmployeeId = e,
                                FirstName = employeeDataService.FindById(e).Result.FirstName,
                                MiddleName = employeeDataService.FindById(e).Result.MiddleName,
                                LastName = employeeDataService.FindById(e).Result.LastName,
                                Salary = employeeDataService.FindById(e).Result.Salary,
                                JobTitle = jobTitleDataService.FindById(employeeDataService.FindById(e).Result.JobTitleId).Name,
                                //Department = depatmentDataService.FindById(depatmentDataService.FindById(e).DepartmentId).DepartmentName,
                            });

                var projectModel = new ProjectViewModel
                {
                    Name = project.Name,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    IsCompleated = project.IsCompleated,
                    EmployeesInProject = employees
                };

                projects.Add(projectModel);
            }

            var viewModel = new AllProjectsViewModel { AllProjects = projects };

            return View(viewModel);
        }
    }
}
