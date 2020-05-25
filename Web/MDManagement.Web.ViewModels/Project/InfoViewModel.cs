using MDManagement.Web.ViewModels.Management;
using System;
using System.Collections.Generic;

namespace MDManagement.Web.ViewModels.Project
{
    public class InfoViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<EmployeeViewModel> EmployeesInProject { get; set; }
    }
}
