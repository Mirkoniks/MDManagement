namespace MDManagement.Web.ViewModels.Project
{
    using System.Collections.Generic;

    public class AssignViewModel
    {
        public string EmployeeId { get; set; }

        public IEnumerable<ProjectViewModel> AllProjects { get; set; }
    }
}
