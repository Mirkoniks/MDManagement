namespace MDManagement.Services.Models.Project
{
    using System;
    using MDManagement.Services.Models.Town;

    public class ProjectServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsCompleated { get; set; }

        public int? CompanyId { get; set; }

        public string ProjectManager { get; set; }
    }
}

