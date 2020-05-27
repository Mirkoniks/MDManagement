namespace MDManagement.Services.Implementations
{
    using MDManagement.Data.Models;
    using MDManagement.Services.Data;
    using MDManagement.Services.Models.Employee;
    using MDManagement.Web.Data;
    using MDManagement.Web.ViewModels.Management;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDataService employeeDataService;
        private readonly IJobTitleService jobTitleService;
        private readonly IJobTitleDataService jobTitleDataService;
        private readonly IDepatmentDataService depatmentDataService;
        private readonly ITownDataService townDataService;
        private readonly IAddressDataService addressDataService;
        private readonly MDManagementDbContext data;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Employee> userManager;

        public EmployeeService(IEmployeeDataService employeeDataService,
                               IJobTitleService jobTitleService,
                               IJobTitleDataService jobTitleDataService,
                               IDepatmentDataService depatmentDataService,
                               ITownDataService townDataService,
                               IAddressDataService addressDataService,
                               MDManagementDbContext data,
                               RoleManager<IdentityRole> roleManager,
                               UserManager<Employee> userManager)
        {
            this.employeeDataService = employeeDataService;
            this.jobTitleService = jobTitleService;
            this.jobTitleDataService = jobTitleDataService;
            this.depatmentDataService = depatmentDataService;
            this.townDataService = townDataService;
            this.addressDataService = addressDataService;
            this.data = data;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task AddManagerRole(ClaimsPrincipal thisUser)
        {
            string roleName = "Manager";

            bool roleExists = await roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                await roleManager.CreateAsync(role);
            }

            var employeeId = userManager.GetUserId(thisUser);

            var user = await employeeDataService.FindById(employeeId);

            await userManager.AddToRoleAsync(user, "Manager");
        }

        public AllEployeesViewModel GetAllEmployees(int? companyId, string userId)
        {
            var allEmployeesViewModel = new AllEployeesViewModel()
            {
                Employees = employeeDataService.GetAllEmployees(companyId, userId)
                .Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    Salary = e.Salary,
                    JobTitle = jobTitleDataService.FindById(e.JobTitleId).Name,
                    Department = depatmentDataService.FindById(e.DepartmentId).DepartmentName,
                    IsCompanyConfirmed = e.IsCompanyConfirmed,
                    Subordinates = e.Employees.Select(e => new EmployeeViewModel
                    {
                        EmployeeId = e.EmployeeId,
                        FirstName = e.FirstName,
                        MiddleName = e.MiddleName,
                        LastName = e.LastName,
                        Salary = e.Salary,
                        JobTitle = jobTitleDataService.FindById(e.JobTitleId).Name,
                        Department = depatmentDataService.FindById(e.DepartmentId).DepartmentName,
                        IsCompanyConfirmed = e.IsCompanyConfirmed
                    })
                    .ToArray()
                })
                .ToArray()
            };

            return allEmployeesViewModel;
        }

        public async Task<EditUserViewModel> EditUserAsync(string employeeId)
        {
            var employee = await employeeDataService.FindById(employeeId);


            var editUserViewModel = new EditUserViewModel()
            {
                EmployeeId = employeeId,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                HireDate = employee.HireDate,
                Salary = employee.Salary,
                JobTitle = jobTitleDataService.FindById(employee.JobTitleId).Name,
                Department = depatmentDataService.FindById(employee.DepartmentId).DepartmentName,
                ManagerNickname = employeeDataService.FindById(employee.ManagerId).Result.UserName,
                IsEmployee = userManager.IsInRoleAsync(employee, "Employee").Result,
                IsManager = userManager.IsInRoleAsync(employee, "Manger").Result,
            };

            return editUserViewModel;
        }

        public EditUserServiceModel EditUser(EditUserViewModel model)
        {
            var editUserServiceModel = new EditUserServiceModel()
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Salary = model.Salary,
                HireDate = model.HireDate,
                IsEmployee = model.IsEmployee,
                IsManager = model.IsManager
            };

            if (!jobTitleDataService.Exists(model.JobTitle))
            {
                jobTitleDataService.CreateJobTitile(model.JobTitle);

                editUserServiceModel.JobTitleId = jobTitleDataService.FindByName(model.JobTitle).Id;
            }
            else
            {
                editUserServiceModel.JobTitleId = jobTitleDataService.FindByName(model.JobTitle).Id;
            }


            if (!depatmentDataService.Exists(model.Department))
            {
                depatmentDataService.Create(model.Department);

                editUserServiceModel.DepartmentId = depatmentDataService.FindByName(model.Department).DepartmentId;
            }
            else
            {
                editUserServiceModel.DepartmentId = depatmentDataService.FindByName(model.Department).DepartmentId;
            }


            return editUserServiceModel;
        }
        public AllUnconfirmedEmployeeViewModel UnconfirmedEmployees(int? companyId)
        {
            var allUnconfirmedEmployeesViewModel = new AllUnconfirmedEmployeeViewModel
            {
                AllUnconfirmedEmployees = employeeDataService.GetAllUnconfirmedEmployees(companyId)
                .Select(u => new UnconfirmedEmployeeViewModel
                {
                    EmployeeId = u.EmployeeId,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    Salary = u.Salary,
                    HireDate = u.HireDate,
                    Town = townDataService.FindById(addressDataService.FindById(u.AdressId).TownId).Name,
                    Address = addressDataService.FindById(u.AdressId).AddressText,
                    JobTitle = jobTitleDataService.FindById(u.JobTitleId).Name,
                    Department = depatmentDataService.FindById(u.DepartmentId).DepartmentName,
                    Manager = employeeDataService.FindByIdTheUserName(u.ManagerId),
                })
                .ToArray()
            };

            return allUnconfirmedEmployeesViewModel;
        }

        public ConfirmEmployeeViewModel ConfirmEmployee(string employeeId)
        {
            var user = employeeDataService.GetUncoFirmedEmployee(employeeId);

            var UnconfirmedEmployeeViewModel = new ConfirmEmployeeViewModel()
            {
                EmployeeId = user.EmployeeId,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                HireDate = user.HireDate,
                Salary = user.Salary,
                Town = townDataService.FindById(addressDataService.FindById(user.AdressId).TownId).Name,
                Address = addressDataService.FindById(user.AdressId).AddressText,
                JobTitle = jobTitleDataService.FindById(user.JobTitleId).Name,
                Department = depatmentDataService.FindById(user.DepartmentId).DepartmentName,
                ManagerNickname = employeeDataService.FindByIdTheUserName(user.ManagerId)
            };

            return UnconfirmedEmployeeViewModel;
        }

    }
}

