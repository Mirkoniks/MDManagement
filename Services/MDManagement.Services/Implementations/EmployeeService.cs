namespace MDManagement.Services.Implementations
{
    using MDManagement.Services.Data;
    using MDManagement.Services.Models.Employee;
    using MDManagement.Web.Data;
    using System.Linq;

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDataService employeeDataService;
        private readonly IJobTitleService jobTitleService;
        private readonly IJobTitleDataService jobTitleDataService;
        private readonly MDManagementDbContext data;

        public EmployeeService(IEmployeeDataService employeeDataService,
                               IJobTitleService jobTitleService,
                               IJobTitleDataService jobTitleDataService,
                               MDManagementDbContext data)
        {
            this.employeeDataService = employeeDataService;
            this.jobTitleService = jobTitleService;
            this.jobTitleDataService = jobTitleDataService;
            this.data = data;
        }

        public void AddEmployeeToCompany(AddCompanyToEmployeeServiceModel model)
        {
            employeeDataService.AddCompanyToEmployee(model);
        }

        public void AddJobTitleToEmployee(string name, string employeeId)
        {
          //  jobTitleService.AddJobTitle(name);

            var jobTitle = jobTitleDataService.FindByName(name);

            data.Users.Where(u => u.Id == employeeId).FirstOrDefault().JobTitleId = jobTitle.Id;
        }
    }
}
