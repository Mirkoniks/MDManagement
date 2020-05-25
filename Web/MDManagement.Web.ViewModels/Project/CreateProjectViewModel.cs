namespace MDManagement.Web.ViewModels.Project
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateProjectViewModel
    {   
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Project Manager Nickname")]
        public string ProjectManagerNick { get; set; }
    }
}
