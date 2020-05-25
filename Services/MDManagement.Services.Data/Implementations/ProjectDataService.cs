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

        public void Join(JoinServiceModel model)
        {
            var employeeProject = new EmployeeProject
            {
                Employee = employeeDataService.FindById(model.EmployeeId).Result,
                Project = FindByCode(model.ProjectCode)
            };
        }

        public Project FindByCode(string projectCode)
        {
            var project = data.Projects
                         .Where(p => p.ProjectCode == projectCode)
                         .FirstOrDefault();

            return project;
        }

        public bool Exists(string projectCode)
        {
            return data.Projects.Any(p => p.ProjectCode == projectCode);
        }

        public IEnumerable<string> GetAllEmployeesInProject(int projectId)
        {
            var employees = data.EmployeeProjects
                            .Where(ep => ep.ProjectId == projectId)
                            .Select(e => e.EmployeeId)
                            .ToArray();

            return employees;
        }

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
        public bool IsEmployeeInProject(string employeeId, int projectId)
        {
            var result = data.EmployeeProjects
                         .Any(ep => ep.EmployeeId == employeeId
                              && ep.ProjectId == projectId);

            return result;
        }

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
