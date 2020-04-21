namespace MDManagement.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    using static MDManagement.Common.DataValidations.Employee;

    public class Employee : IdentityUser
    {
        [Required]
        [MaxLength(NameMaxValue)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxValue)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(NameMaxValue)]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Column(TypeName = SalaryDeciamlSecifications)]
        public decimal Salary { get; set; }
    }
}
