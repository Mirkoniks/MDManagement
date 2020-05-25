namespace MDManagement.Services.Data
{
    using MDManagement.Data.Models;
    using MDManagement.Services.Data.Implementations;
    using MDManagement.Services.Models.Employee;
    using MDManagement.Services.Models.Project;
    using MDManagement.Services.Models.Town;
    using System.Collections.Generic;

    public interface IProjectDataService
    {
        public IEnumerable<EmployeeProjectServiceModel> GetAllProjects(int? companyId);

        public ProjectServiceModel FindProjectById(int id);

        public void Create(ProjectServiceModel model);

        public void Join(JoinServiceModel model);

        public Project FindByCode(string projectCode);

        public bool Exists(string projectCode);

        public IEnumerable<string> GetAllEmployeesInProject(int projectId);

        public void Assign(string employeeId, int projectId);

        public bool IsEmployeeInProject(string employeeId, int projectId);

        public void Edit(ProjectServiceModel model);

        public IEnumerable<EmployeeProjectServiceModel> GetEmployeeProjects(string employeeId);
    }
}
