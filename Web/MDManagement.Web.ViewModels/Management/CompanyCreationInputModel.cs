namespace MDManagement.Web.ViewModels.Management
{
    using MDManagement.Data.Models;
    public class CompanyCreationInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CurrentEmployeeId { get; set; }

        public string BossTitle { get; set; }

    }
}
