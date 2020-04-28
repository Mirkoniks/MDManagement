namespace MDManagement.Services.Data
{
    using MDManagement.Services.Models.Employee;

    public interface IEmployeeDataService
    {
        public void AddCompanyToEmployee(AddCompanyToEmployeeServiceModel model);

        public void AddJobTitleToEployee(AddJobTitleToEmployeServiceModel addJobTitleToEmployeServiceModel);

        //public void AddEmployeeToCompany(string employeeId, string companyCode);

        //public void AddJobTitleToEmployee(string employeeId, string jobTtitle);
    }
}
