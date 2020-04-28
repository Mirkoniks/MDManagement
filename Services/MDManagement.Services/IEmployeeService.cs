namespace MDManagement.Services
{
    using MDManagement.Services.Models.Employee;

    public interface IEmployeeService
    {
        public void AddEmployeeToCompany(AddCompanyToEmployeeServiceModel model);

        public void AddJobTitleToEmployee(string name, string employeeId);
    }
}
