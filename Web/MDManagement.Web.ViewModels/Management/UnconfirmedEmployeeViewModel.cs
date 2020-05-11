namespace MDManagement.Web.ViewModels.Management
{
    using System;

    public class UnconfirmedEmployeeViewModel
    {
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        public decimal? Salary { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }

        public string JobTitle { get; set; }

        public string Department { get; set; }

        public string Manager { get; set; }
    }
}
