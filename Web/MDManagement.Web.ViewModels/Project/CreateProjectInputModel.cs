namespace MDManagement.Web.ViewModels.Project
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateProjectInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Project Manager Nickname")]
        public string ProjectManagerNick { get; set; }
    }
}
