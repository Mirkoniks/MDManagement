namespace MDManagement.Services.Implementations
{
    using MDManagement.Services;
    using MDManagement.Services.Data;
    using MDManagement.Services.Models.Company;
    using MDManagement.Web.Data;
    using System.Linq;

    public class ComapnyService : IComapnyService
    {
        private readonly ICompanyDataService companyDataService;
        private readonly MDManagementDbContext data;

        public ComapnyService(ICompanyDataService companyDataService,
                              MDManagementDbContext data)
        {
            this.companyDataService = companyDataService;
            this.data = data;
        }

        public void Register(CreateCompanyServiceModel model)
        {
            companyDataService.Create(model);
        }

        public CompanyServiceModel FindByName(string name)
        {
            var company = data.Companies.Where(c => c.Name == name).FirstOrDefault();

            var csm = new CompanyServiceModel()
            {
                Name = company.Name,
                Description = company.Description,
                CompanyCode = company.CompanyCode
            };

            return csm;
        }

        public bool Exists(string companyName)
        {
            return data.Companies.Any(c => c.Name.ToLower() == companyName.ToLower());
        }
    }
}
