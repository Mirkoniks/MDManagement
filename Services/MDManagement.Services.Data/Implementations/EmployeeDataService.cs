namespace MDManagement.Services.Data.Implementations
{
    using System.Linq;
    using MDManagement.Services.Models.Employee;
    using MDManagement.Web.Data;

    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly MDManagementDbContext data;
        private readonly IJobTitleDataService jobTitleDataService;

        public EmployeeDataService(MDManagementDbContext data,
                                   IJobTitleDataService jobTitleDataService)
        {
            this.data = data;
            this.jobTitleDataService = jobTitleDataService;

        }
        public void AddCompanyToEmployee(AddCompanyToEmployeeServiceModel model)
        {
            data.Users.Where(e => e.Id == model.EmployeeId).FirstOrDefault().CompanyId = model.CompanyId;
            data.SaveChanges();
        }

        public void AddJobTitleToEployee(AddJobTitleToEmployeServiceModel addJobTitleToEmployeServiceModel)
        {
            data.Users.Where(e => e.Id == addJobTitleToEmployeServiceModel.EmployeeId).FirstOrDefault().JobTitleId = addJobTitleToEmployeServiceModel.JobTitleId;
            data.SaveChanges();
        }



        //public void AddEmployeeToCompany(string employeeId, string companyCode)
        //{
        //    int companyId = data.Companies.Where(c => c.CompanyCode == companyCode).FirstOrDefault().Id;

        //    data.Users.Where(u => u.Id == employeeId).FirstOrDefault().CompanyId = companyId;

        //    data.SaveChanges();
        //}

        //public void AddJobTitleToEmployee(string employeeId, string jobTtitle)
        //{
        //    string userId = data.Users.Where(u => u.Id == employeeId).FirstOrDefault().Id;

        //    int jobTitleId = jobTitleDataService.Exists(jobTtitle);

        //    data.Users.Where(u => u.Id == userId).FirstOrDefault().JobTitleId = jobTitleId;
        //}
    }
}
