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

        /// <summary>
        /// Adds compnay to a employee
        /// </summary>
        /// <param name="model">AddCompanyToEmployeeServiceModel is a DTO which contains the needed info for this operations</param>
        public void AddCompanyToEmployee(AddCompanyToEmployeeServiceModel model)
        {
            data.Users.Where(e => e.Id == model.EmployeeId).FirstOrDefault().CompanyId = model.CompanyId;
            data.SaveChanges();
        }

        /// <summary>
        /// Adds Job Title to employee
        /// </summary>
        /// <param name="addJobTitleToEmployeServiceModel">AddJobTitleToEmployeServiceModel is a DTO which contains the needed info for this operations</param>
        public void AddJobTitleToEployee(AddJobTitleToEmployeServiceModel addJobTitleToEmployeServiceModel)
        {
            data.Users.Where(e => e.Id == addJobTitleToEmployeServiceModel.EmployeeId).FirstOrDefault().JobTitleId = addJobTitleToEmployeServiceModel.JobTitleId;
            data.SaveChanges();
        }

        /// <summary>
        /// Find employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee</returns>
        public async Task<Employee> FindById(string id)
        {
            return await data.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all employees and their subordinates
        /// </summary>
        /// <param name="companyId">Company id</param>
        /// <param name="userId">User id</param>
        /// <param name="userManagerId">Manager id</param>
        /// <returns>List of EmployeeService model which is a DTO which contains the needed info for this operations</returns>
        public IEnumerable<EmployeeServiceModel> GetAllEmployees(int? companyId, string userId, string userManagerId)
        {

            var user = data.Users
                .Where(u => u.CompanyId == companyId
                       && u.ManagerId == userManagerId
                       || u.ManagerId == userId
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

        /// <summary>
        /// Gets an employee for edit
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>EditUserServiceModel which is a DTO which contains the needed info for this operations</returns>
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

        /// <summary>
        /// Edits user details
        /// </summary>
        /// <param name="model">EditUserServiceModel which is a DTO which contains the needed info for this operations</param>
        /// <returns></returns>
        public async Task EditUserDetailsAsync(EditUserServiceModel model)
        {
            var user = await data.Users.Where(e => e.Id == model.EmployeeId).FirstOrDefaultAsync();

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
                await userManager.RemoveFromRoleAsync(user, "Employee");
                await userManager.AddToRoleAsync(user, "Manager");
            }
            else if (model.IsEmployee)
            {
                await userManager.RemoveFromRoleAsync(user, "Manager");
                await userManager.AddToRoleAsync(user, "Employee");
            }


            data.SaveChanges();
        }

        /// <summary>
        /// Checks by username if employee really exists
        /// </summary>
        /// <param name="userName">username</param>
        /// <returns>Returns true if exists</returns>
        public bool Exists(string userName)
        {
            return data.Users.Any(u => u.UserName == userName);
        }

        /// <summary>
        /// Checks by id if employee really exists
        /// </summary>
        /// <param name="Id">id</param>
        /// <returns>Returns true if exists</returns>
        public bool ExistsId(string Id)
        {
            return data.Users.Any(u => u.Id == Id);
        }

        /// <summary>
        /// Finds an employee by username
        /// </summary>
        /// <param name="nickName">username</param>
        /// <returns>Employee</returns>
        public string FindByNickname(string nickName)
        {
            return data.Users.Where(u => u.UserName == nickName).FirstOrDefault().Id;
        }

        /// <summary>
        /// Gets all unconfirmed employees
        /// </summary>
        /// <param name="companyId">Id for the specific company</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets unconfirmed employee
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>UnconfirmedEmployeeServiceModel which is a DTO which contains the needed info for this operations</returns>
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

        /// <summary>
        /// Cheks if the employee's address is null
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>Returns true if address id null</returns>
        public bool IsAddressNull(string employeeId)
        {
            return data.Users.Where(e => e.Id == employeeId).FirstOrDefault().Address == null;
        }

        /// <summary>
        /// Finds employee's username by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee's username</returns>
        public string FindByIdTheUserName(string id)
        {
            if (!ExistsId(id))
            {
                return " ";
            }

            return data.Users.Where(u => u.Id == id).FirstOrDefault().UserName;
        }

        /// <summary>
        /// Confirms an employee
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns></returns>
        public async Task ConfirmEmployee(string id)
        {
            data.Users.Where(u => u.Id == id)
                .FirstOrDefaultAsync()
                .Result
                .IsCompanyConfirmed = true;

            await data.SaveChangesAsync();
        }

        /// <summary>
        /// Removes company from an employee
        /// </summary>
        /// <param name="emplolyeeId">Employee id</param>
        /// <returns></returns>
        public async Task RemoveEmployeeCompany(string emplolyeeId)
        {
            data.Users.Where(e => e.Id == emplolyeeId)
                .FirstOrDefaultAsync()
                .Result
                .CompanyId = null;

            await data.SaveChangesAsync();
        }

        /// <summary>
        /// Removes Manager from an employee
        /// </summary>
        /// <param name="emplolyeeId">Employee id</param>
        /// <returns></returns>
        public async Task RemoveEmployeeMnager(string emplolyeeId)
        {
            data.Users.Where(e => e.Id == emplolyeeId)
                .FirstOrDefaultAsync()
                .Result
                .ManagerId = null;

            await data.SaveChangesAsync();
        }

        /// <summary>
        /// Removes deparment from an employee
        /// </summary>
        /// <param name="emplolyeeId">Employee id</param>
        /// <returns></returns>
        public async Task RemoveEmployeeDepartmentAsync(string emplolyeeId)
        {
            data.Users.Where(e => e.Id == emplolyeeId)
                .FirstOrDefaultAsync()
                .Result
                .DepartmentId = null;

            await data.SaveChangesAsync();
        }

        /// <summary>
        /// Removes Job Title from an employee
        /// </summary>
        /// <param name="emplolyeeId">Employee id</param>
        /// <returns></returns>
        public async Task RemoveEmployeeJobTitle(string emplolyeeId)
        {
            data.Users.Where(e => e.Id == emplolyeeId)
                .FirstOrDefaultAsync()
                .Result
                .JobTitleId = null;

            await data.SaveChangesAsync();
        }

        /// <summary>
        /// Removes Company confirmation from an employee
        /// </summary>
        /// <param name="emplolyeeId">Employee id</param>
        /// <returns></returns>
        public async Task RemoveEmployeeCompanyConfirmantion(string emplolyeeId)
        {
            data.Users.Where(e => e.Id == emplolyeeId)
                .FirstOrDefaultAsync()
                .Result
                .IsCompanyConfirmed = false;

            await data.SaveChangesAsync();
        }

        /// <summary>
        /// Removes all projects from an employee
        /// </summary>
        /// <param name="emplolyeeId">Employee id</param>
        /// <returns></returns>
        public async Task RemoveEmployeeProjects(string emplolyeeId)
        {
            var employeesToDelete = data.EmployeeProjects.
                Where(e => e.EmployeeId == emplolyeeId);

            foreach (var employee in employeesToDelete)
            {
                data.EmployeeProjects.Remove(employee);
            }

            await data.SaveChangesAsync();
        }

        /// <summary>
        /// Gets employee's salary
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns></returns>
        public decimal? GetEmployeeSalary(string employeeId)
        {
            return data.Users.Where(u => u.Id == employeeId)
                .FirstOrDefaultAsync()
                .Result
                .Salary;
        }

        /// <summary>
        /// Sets employee's manager
        /// </summary>
        /// <param name="managerId">Manger id</param>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        public async Task SetManager(string managerId, string userId)
        {
            data.Users.Where(e => e.Id == userId)
                .FirstOrDefaultAsync()
                .Result
                .ManagerId = managerId;

            await data.SaveChangesAsync();
        }

        /// <summary>
        /// Remove all roles from an emoployee
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns></returns>
        public async Task RemoveRoles(string employeeId)
        {
            var user = await data.Users
                      .Where(u => u.Id == employeeId)
                      .FirstOrDefaultAsync();

            if (await userManager.IsInRoleAsync(user, "Manager"))
            {
                await userManager.RemoveFromRoleAsync(user, "Manager");
            }
            else if (await userManager.IsInRoleAsync(user, "Employee"))
            {
                await userManager.RemoveFromRoleAsync(user, "Employee");
            }
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
