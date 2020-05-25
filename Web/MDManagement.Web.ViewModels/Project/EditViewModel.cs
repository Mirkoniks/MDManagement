namespace MDManagement.Web.ViewModels.Project
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Is Project Compleated")]
        public bool IsCompleated { get; set; }
    }
}
