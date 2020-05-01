namespace MDManagement.Services.Data
{
    using MDManagement.Data.Models;
    using MDManagement.Services.Models.Employee;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmployeeDataService
    {
        public void AddCompanyToEmployee(AddCompanyToEmployeeServiceModel model);

        public void AddJobTitleToEployee(AddJobTitleToEmployeServiceModel addJobTitleToEmployeServiceModel);

        public Task<Employee> FindById(string id);

        public IEnumerable<EmployeeServiceModel> GetAllEmployees(int? companyId);

        public Task<EditUserServiceModel> GetEmployeeByIdForEdit(string userId);

        public void EditUserDetails(EditUserServiceModel model);

        public bool Exists(string userName);

        public string FindByNickname(string nickName);



        //public void AddEmployeeToCompany(string employeeId, string companyCode);

        //public void AddJobTitleToEmployee(string employeeId, string jobTtitle);
    }
}
