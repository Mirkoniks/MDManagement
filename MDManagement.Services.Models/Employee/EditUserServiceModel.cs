namespace MDManagement.Services.Models.Employee
{
    using System;
    using System.Reflection;

    public class EditUserServiceModel
    {
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        public decimal? Salary { get; set; }

        public int? JobTitleId { get; set; }

        public int? DepartmentId { get; set; }

        public string ManagerId { get; set; }

        public bool IsManager { get; set; }

        public bool IsEmployee { get; set; }
    }
}
