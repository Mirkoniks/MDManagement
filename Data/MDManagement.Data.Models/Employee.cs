namespace MDManagement.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Net.Http.Headers;
    using Microsoft.AspNetCore.Identity;

    using static MDManagement.Common.DataValidations.Employee;

    public class Employee : IdentityUser
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Column(TypeName = SalaryDeciamlSecifications)]
        public decimal Salary { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public int JobTitleId { get; set; }

        public JobTitle JobTitle { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new HashSet<EmployeeProject>();

        public Address Address { get; set; }
    }
}
