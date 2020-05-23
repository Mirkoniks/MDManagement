namespace MDManagement.Web.ViewModels.Management
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class EditUserViewModel
    {
        public string EmployeeId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        public decimal? Salary { get; set; }

        public string JobTitle { get; set; }

        [Display(Name = "Manager")]
        public bool IsManager { get; set; }

        [Display(Name = "Employee")]
        public bool IsEmployee { get; set; }

        [Required]       
        public string Department { get; set; }

        public string ManagerNickname { get; set; }
    }
}
