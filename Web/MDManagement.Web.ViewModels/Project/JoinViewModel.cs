namespace MDManagement.Web.ViewModels.Project
{
    using System.ComponentModel.DataAnnotations;

    public class JoinViewModel
    {
        [Required]
        [Display(Name = "Project Join Code")]
        public string ProjectCode { get; set; }
    }
}
