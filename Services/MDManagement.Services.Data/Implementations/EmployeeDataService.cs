namespace MDManagement.Services.Data.Implementations
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MDManagement.Data.Models;
    using MDManagement.Services.Models.Employee;
    using MDManagement.Web.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

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

        public async Task<Employee> FindById(string id)
        {
            return await data.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<EmployeeServiceModel> GetAllEmployees(int? companyId)
        {
            return data.Users
                .Where(u => u.CompanyId == companyId)
                .Select(e => new EmployeeServiceModel
                {
                    EmployeeId = e.Id,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    Salary = e.Salary,
                    JobTitle = e.JobTitle.Name,
                    Department = e.Department.Name
                })
                .ToArray();
        }

        public async Task<EditUserServiceModel> GetEmployeeByIdForEdit(string userId)
        {
            return await data.Users.Where(u => u.Id == userId)
                .Select(e => new EditUserServiceModel 
                {
                    EmployeeId = e.Id,
                    HireDate = e.HireDate,
                    Salary = e.Salary,
                    JobTitleId = e.JobTitleId,
                    DepartmentId = e.DepartmentId,
                    ManagerId = e.ManagerId
                })
                .FirstOrDefaultAsync();
        }

        public void EditUserDetails(EditUserServiceModel model)
        {
            var user = data.Users.Where(e => e.Id == model.EmployeeId).FirstOrDefault();
           
            user.HireDate = model.HireDate;
            user.Salary = model.Salary;
            user.JobTitleId = model.JobTitleId;
            user.DepartmentId = model.DepartmentId;
            user.ManagerId = model.ManagerId;

            data.Users.Update(user);

            data.SaveChanges();
        }

        public bool Exists(string userName)
        {
           return data.Users.Any(u => u.UserName == userName);
        }

        public string FindByNickname(string nickName)
        {
            return data.Users.Where(u => u.UserName == nickName).FirstOrDefault().Id;
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
