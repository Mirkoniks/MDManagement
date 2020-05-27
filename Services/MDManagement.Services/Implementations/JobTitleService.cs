using MDManagement.Data.Models;
using MDManagement.Services.Data;
using MDManagement.Services.Models.Employee;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MDManagement.Services.Implementations
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleDataService jobTitleDataService;
        private readonly IEmployeeDataService employeeDataService;
        private readonly UserManager<Employee> userManager;

        public JobTitleService(IJobTitleDataService jobTitleDataService,
                               IEmployeeDataService employeeDataService,
                               UserManager<Employee> userManager)
        {
            this.jobTitleDataService = jobTitleDataService;
            this.employeeDataService = employeeDataService;
            this.userManager = userManager;
        }

        public void AddJobTitle(string name, ClaimsPrincipal user)
        {
            if (!jobTitleDataService.Exists(name))
            {
                jobTitleDataService.CreateJobTitile(name.ToString());

                var companyId = jobTitleDataService.FindByName(name).Id;

                var addJobTitleToEmployeServiceModel = new AddJobTitleToEmployeServiceModel()
                {
                    EmployeeId = userManager.GetUserId(user),
                    JobTitleId = companyId
                };

                employeeDataService.AddJobTitleToEployee(addJobTitleToEmployeServiceModel);
            }
            else
            {
                var companyId = jobTitleDataService.FindByName(name).Id;

                var addJobTitleToEmployeServiceModel = new AddJobTitleToEmployeServiceModel()
                {
                    EmployeeId = userManager.GetUserId(user),
                    JobTitleId = companyId
                };

                employeeDataService.AddJobTitleToEployee(addJobTitleToEmployeServiceModel);
            }
        }
    }
}
