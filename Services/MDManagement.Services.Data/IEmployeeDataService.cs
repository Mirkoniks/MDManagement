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

        public void EditUserDetailsAsync(EditUserServiceModel model);

        public bool Exists(string userName);

        public bool ExistsId(string id);

        public string FindByNickname(string nickName);

        public string FindByIdTheUserName(string id);

        public IEnumerable<UnconfirmedEmployeeServiceModel> GetAllUnconfirmedEmployees(int? companyCode);

        public bool IsAddressNull(string employeeId);

        public UnconfirmedEmployeeServiceModel GetUncoFirmedEmployee(string id);

        public void ConfirmEmployee(string id);


        //public void AddEmployeeToCompany(string employeeId, string companyCode);

        //public void AddJobTitleToEmployee(string employeeId, string jobTtitle);
    }
}
