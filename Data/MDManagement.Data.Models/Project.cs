namespace MDManagement.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static MDManagement.Common.DataValidations.Project;

    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsCompleated { get; set; }

        public string ProjectCode { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new HashSet<EmployeeProject>();

        public int? CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
