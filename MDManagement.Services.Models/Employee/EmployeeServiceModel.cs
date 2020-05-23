
namespace MDManagement.Services.Models.Employee
{
    using System.Collections.Generic;
    using MDManagement.Data.Models;

    public class EmployeeServiceModel
    {
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public decimal? Salary { get; set; }

        public int? JobTitleId { get; set; }

        public int? DepartmentId { get; set; }

        public bool IsCompanyConfirmed { get; set; }

        public IEnumerable<EmployeeServiceModel> Employees { get; set; }
    }
}
