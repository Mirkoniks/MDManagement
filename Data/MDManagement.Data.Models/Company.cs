namespace MDManagement.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static MDManagement.Common.DataValidations.Company;

    public class Company
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(CompanyCodeMaxValue)]
        public string CompanyCode { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
