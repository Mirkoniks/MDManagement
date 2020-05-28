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

        public IEnumerable<EmployeeServiceModel> GetAllEmployees(int? companyId,string userId);

        public Task<EditUserServiceModel> GetEmployeeByIdForEdit(string userId);

        public Task EditUserDetailsAsync(EditUserServiceModel model);

        public bool Exists(string userName);

        public bool ExistsId(string id);

        public string FindByNickname(string nickName);

        public string FindByIdTheUserName(string id);

        public IEnumerable<UnconfirmedEmployeeServiceModel> GetAllUnconfirmedEmployees(int? companyCode);

        public bool IsAddressNull(string employeeId);

        public UnconfirmedEmployeeServiceModel GetUncoFirmedEmployee(string id);

        public Task ConfirmEmployee(string id);

        public Task RemoveEmployeeCompany(string emplolyeeId);

        public Task RemoveEmployeeMnager(string employeeId);

        public Task RemoveEmployeeDepartmentAsync(string emplolyeeId);

        public Task RemoveEmployeeJobTitle(string emplolyeeId);

        public Task RemoveEmployeeCompanyConfirmantion(string emplolyeeId);

        public Task RemoveEmployeeProjects(string emplolyeeId);

        public decimal? GetEmployeeSalary(string employeeId);

        public Task SetManager(string managerId, string userId);

        //public void AddEmployeeToCompany(string employeeId, string companyCode);

        //public void AddJobTitleToEmployee(string employeeId, string jobTtitle);
    }
}
