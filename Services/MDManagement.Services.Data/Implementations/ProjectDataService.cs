namespace MDManagement.Services.Data.Implementations
{
    using MDManagement.Data.Models;
    using MDManagement.Services.Models.Employee;
    using MDManagement.Services.Models.Project;
    using MDManagement.Services.Models.Town;
    using MDManagement.Web.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProjectDataService : IProjectDataService
    {
        private readonly MDManagementDbContext data;
        private readonly IEmployeeDataService employeeDataService;

        public ProjectDataService(MDManagementDbContext data,
                                  IEmployeeDataService employeeDataService)
        {
            this.data = data;
            this.employeeDataService = employeeDataService;
        }

        /// <summary>
        /// Gets all company projects
        /// </summary>
        /// <param name="companyId">Company id</param>
        /// <returns>List of EmployeeProjectServiceModel which is a DTO which contains the needed info for this operations</returns>
        public IEnumerable<EmployeeProjectServiceModel> GetAllProjects(int? companyId)
        {
            var model = data.EmployeeProjects
                        .Where(ep => ep.Employee.CompanyId == companyId
                                && ep.Project.IsCompleated == false)
                        .Select(e => new EmployeeProjectServiceModel
                        {
                            ProjectId = e.ProjectId
                        })
                        .Distinct()
                        .ToArray();

            return model;
        }

        /// <summary>
        /// Finds a proejct by id
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>ProjectServiceModel which is a DTO which contains the needed info for this operations</returns>
        public ProjectServiceModel FindProjectById(int id)
        {
            var project = data.Projects
                          .Where(p => p.Id == id)
                          .Select(p => new ProjectServiceModel
                          {
                              Id = p.Id,
                              Name = p.Name,
                              Description = p.Description,
                              StartDate = p.StartDate,
                              EndDate = p.EndDate,
                              IsCompleated = p.IsCompleated
                          })
                          .FirstOrDefault();

            return project;
        }

        /// <summary>
        /// Creates a project
        /// </summary>
        /// <param name="model">Project name</param>
        public void Create(ProjectServiceModel model)
        {
            var project = new Project
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CompanyId = model.CompanyId,
                ProjectCode = $"#{DateTime.UtcNow.Day}{DateTime.UtcNow.Hour}{DateTime.UtcNow.Minute}{DateTime.UtcNow.Millisecond}"
            };

            data.Projects.Add(project);

            var employeeProject = new EmployeeProject
            {
                Employee = employeeDataService.FindById(model.ProjectManager).Result,
                Project = project
            };

            data.EmployeeProjects.Add(employeeProject);

            data.SaveChanges();
        }

        /// <summary>
        /// Joinig a project 
        /// </summary>
        /// <param name="model">JoinServiceModel which is a DTO which contains the needed info for this operations</param>
        public void Join(JoinServiceModel model)
        {
            var employeeProject = new EmployeeProject
            {
                Employee = employeeDataService.FindById(model.EmployeeId).Result,
                Project = FindByCode(model.ProjectCode)
            };
        }

        /// <summary>
        /// Finds a project by project code
        /// </summary>
        /// <param name="projectCode">Project code</param>
        /// <returns></returns>
        public Project FindByCode(string projectCode)
        {
            var project = data.Projects
                         .Where(p => p.ProjectCode == projectCode)
                         .FirstOrDefault();

            return project;
        }

        /// <summary>
        /// Checks by project code if project really exists
        /// </summary>
        /// <param name="projectCode">Project code</param>
        /// <returns>Returns true if exists</returns>
        public bool Exists(string projectCode)
        {
            return data.Projects.Any(p => p.ProjectCode == projectCode);
        }

        /// <summary>
        /// Gets all projects with employees in it
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <returns>Returns employees ids</returns>
        public IEnumerable<string> GetAllEmployeesInProject(int projectId)
        {
            var employees = data.EmployeeProjects
                            .Where(ep => ep.ProjectId == projectId)
                            .Select(e => e.EmployeeId)
                            .ToArray();

            return employees;
        }

        /// <summary>
        /// Assigns an employee to a project
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <param name="projectId">Project id</param>
        public void Assign(string employeeId, int projectId)
        {
            var model = new EmployeeProject
            {
                EmployeeId = employeeId,
                ProjectId = projectId
            };

            data.EmployeeProjects.Add(model);

            data.SaveChanges();
        }

        /// <summary>
        /// Checks if an employes is in a projects
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <param name="projectId">Project id</param>
        /// <returns>Returns true if it is</returns>
        public bool IsEmployeeInProject(string employeeId, int projectId)
        {
            var result = data.EmployeeProjects
                         .Any(ep => ep.EmployeeId == employeeId
                              && ep.ProjectId == projectId);

            return result;
        }


        /// <summary>
        /// Edits a project
        /// </summary>
        /// <param name="model">ProjectServiceModel which is a DTO which contains the needed info for this operations</param>
        public void Edit(ProjectServiceModel model)
        {
            var project = data.Projects.Where(p => p.Id == model.Id).FirstOrDefault();

            project.Name = model.Name;
            project.Description = model.Description;
            project.StartDate = model.StartDate;
            project.EndDate = model.EndDate;
            project.IsCompleated = model.IsCompleated;

            data.SaveChanges();
        }

        /// <summary>
        /// Gets all projects of an employee
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>List of EmployeeProjectServiceModel which is a DTO which contains the needed info for this operations</returns>
        public IEnumerable<EmployeeProjectServiceModel> GetEmployeeProjects(string employeeId)
        {
            var projects = data.EmployeeProjects
                           .Where(ep => ep.EmployeeId == employeeId)
                           .Select(ep => new EmployeeProjectServiceModel
                           {
                               ProjectId = ep.ProjectId
                           })
                           .ToArray();

            return projects;
        }


    }
}
