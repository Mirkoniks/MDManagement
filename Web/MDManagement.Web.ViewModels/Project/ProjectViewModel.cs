namespace MDManagement.Web.ViewModels.Project
{
    using MDManagement.Web.ViewModels.Management;
    using System;
    using System.Collections.Generic;

    public class ProjectViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsCompleated { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<EmployeeViewModel> EmployeesInProject { get; set; }
    }
}
