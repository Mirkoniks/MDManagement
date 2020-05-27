namespace MDManagement.Services.Data.Implementations
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MDManagement.Data.Models;
    using MDManagement.Services.Models.Employee;
    using MDManagement.Web.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly MDManagementDbContext data;
        private readonly IJobTitleDataService jobTitleDataService;
        private readonly ICompanyDataService companyDataService;
        private readonly UserManager<Employee> userManager;

        public EmployeeDataService(MDManagementDbContext data,
                                   IJobTitleDataService jobTitleDataService,
                                   ICompanyDataService companyDataService,
                                   UserManager<Employee> userManager)
        {
            this.data = data;
            this.jobTitleDataService = jobTitleDataService;
            this.companyDataService = companyDataService;
            this.userManager = userManager;
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



        public IEnumerable<EmployeeServiceModel> GetAllEmployees(int? companyId, string userId)
        {
            var user = data.Users
                .Where(u => u.CompanyId == companyId
                       && u.ManagerId == userId
                       )
                .Select(e => new EmployeeServiceModel
                {
                    EmployeeId = e.Id,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    Salary = e.Salary,
                    JobTitleId = e.JobTitleId,
                    DepartmentId = e.DepartmentId,
                    IsCompanyConfirmed = e.IsCompanyConfirmed,
                    Employees = e.Employees.Select(e => new EmployeeServiceModel
                    {
                        EmployeeId = e.Id,
                        FirstName = e.FirstName,
                        MiddleName = e.MiddleName,
                        LastName = e.LastName,
                        Salary = e.Salary,
                        JobTitleId = e.JobTitleId,
                        DepartmentId = e.DepartmentId,
                        IsCompanyConfirmed = e.IsCompanyConfirmed
                    })
                    .ToArray()
                })
                .ToArray();

            return user;
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

        public void EditUserDetailsAsync(EditUserServiceModel model)
        {
            var user = data.Users.Where(e => e.Id == model.EmployeeId).FirstOrDefault();

            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.HireDate = model.HireDate;
            user.Salary = model.Salary;
            user.JobTitleId = model.JobTitleId;
            user.DepartmentId = model.DepartmentId;
            user.ManagerId = model.ManagerId;

            if (model.IsManager)
            {
                //userManager.RemoveFromRoleAsync(user, "Employee");
                userManager.AddToRoleAsync(user, "Manager");
            }
            else if (model.IsEmployee)
            {
                userManager.RemoveFromRoleAsync(user, "Manager");
                //userManager.AddToRoleAsync(user, "Employee");
            }


            data.SaveChanges();
        }

        public bool Exists(string userName)
        {
            return data.Users.Any(u => u.UserName == userName);
        }


        public bool ExistsId(string Id)
        {
            return data.Users.Any(u => u.Id == Id);
        }

        public string FindByNickname(string nickName)
        {
            return data.Users.Where(u => u.UserName == nickName).FirstOrDefault().Id;
        }

        public IEnumerable<UnconfirmedEmployeeServiceModel> GetAllUnconfirmedEmployees(int? companyId)
        {
            if (companyDataService.HasEmployees(companyId))
            {
                var companyCode = companyDataService.FindById(companyId).CompanyCode;

                var user = data.Users
                   .Where(u => u.IsCompanyConfirmed == false
                          && u.Company.CompanyCode == companyCode)
                   .Select(u => new UnconfirmedEmployeeServiceModel
                   {
                       EmployeeId = u.Id,
                       FirstName = u.FirstName,
                       MiddleName = u.MiddleName,
                       LastName = u.LastName,
                       HireDate = u.HireDate,
                       Salary = u.Salary,
                       AdressId = u.Address.Id,
                       JobTitleId = u.JobTitle.Id,
                       DepartmentId = u.Department.Id,
                       ManagerId = u.ManagerId
                   })
                   .ToArray();

                return user;
            }

            List<UnconfirmedEmployeeServiceModel> users = new List<UnconfirmedEmployeeServiceModel>();

            var model = new UnconfirmedEmployeeServiceModel
            {
                FirstName = "no"
            };

            users.Add(model);

            return users;
        }

        public UnconfirmedEmployeeServiceModel GetUncoFirmedEmployee(string id)
        {
            var employee = data.Users
                .Where(u => u.Id == id)
                .Select(u => new UnconfirmedEmployeeServiceModel
                {
                    EmployeeId = u.Id,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    HireDate = u.HireDate,
                    Salary = u.Salary,
                    AdressId = u.Address.Id,
                    JobTitleId = u.JobTitle.Id,
                    DepartmentId = u.Department.Id,
                    ManagerId = u.ManagerId,
                    IsEmployeeConfirmed = u.IsCompanyConfirmed
                })
                .FirstOrDefault();

            return employee;
        }

        public bool IsAddressNull(string employeeId)
        {
            return data.Users.Where(e => e.Id == employeeId).FirstOrDefault().Address == null;
        }

        public string FindByIdTheUserName(string id)
        {
            if (!ExistsId(id))
            {
                return " ";
            }

            return data.Users.Where(u => u.Id == id).FirstOrDefault().UserName;
        }

        public void ConfirmEmployee(string id)
        {
            data.Users.Where(u => u.Id == id).FirstOrDefault().IsCompanyConfirmed = true;

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
