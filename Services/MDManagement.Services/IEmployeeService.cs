namespace MDManagement.Services
{
    using MDManagement.Services.Models.Employee;
    using MDManagement.Web.ViewModels.Management;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IEmployeeService
    {
        public Task AddManagerRole(ClaimsPrincipal thisUser);

        public AllEployeesViewModel GetAllEmployees(int? companyId, string userId);

        public Task<EditUserViewModel> EditUserAsync(string employeeId);

        public EditUserServiceModel EditUser(EditUserViewModel model);

        public AllUnconfirmedEmployeeViewModel UnconfirmedEmployees(int? companyId);

        public ConfirmEmployeeViewModel ConfirmEmployee(string employeeId);

        public Task RemoveEmployeeFromCompany(string employeeId);
    }
}
