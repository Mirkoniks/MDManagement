using System;
using System.Xml.Linq;

namespace MDManagement.Services.Models.Employee
{
    public class UnconfirmedEmployeeServiceModel
    {
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        public decimal? Salary { get; set; }

        public int AdressId { get; set; }

        public int JobTitleId { get; set; }

        public int DepartmentId { get; set; }

        public string ManagerId { get; set; }

        public bool IsEmployeeConfirmed { get; set; }

    }
}
