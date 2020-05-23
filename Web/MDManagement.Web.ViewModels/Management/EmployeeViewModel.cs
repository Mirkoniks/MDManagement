using MDManagement.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace MDManagement.Web.ViewModels.Management
{
    public class EmployeeViewModel
    {
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public decimal? Salary { get; set; }

        public string JobTitle { get; set; }

        public string Department { get; set; }

        public bool IsCompanyConfirmed { get; set; }

        public IEnumerable<EmployeeViewModel> Subordinates { get; set; }
    }
}
