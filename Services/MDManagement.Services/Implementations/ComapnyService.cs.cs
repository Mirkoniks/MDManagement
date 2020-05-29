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

        /// <summary>
        /// Creates a company
        /// </summary>
        /// <param name="model">CreateCompanyServiceModel which is a DTO which contains the needed info for this operations</param>
        public void Register(CreateCompanyServiceModel model)
        {
            companyDataService.Create(model);
        }

        /// <summary>
        /// Finds a company by name
        /// </summary>
        /// <param name="name">Company name</param>
        /// <returns>CompanyServiceModel which is a DTO which contains the needed info for this operations</returns>
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

        /// <summary>
        /// Checks by company name if a company exists
        /// </summary>
        /// <param name="companyName">Company name</param>
        /// <returns>Returns true if exists</returns>
        public bool Exists(string companyName)
        {
            return data.Companies.Any(c => c.Name.ToLower() == companyName.ToLower());
        }
    }
}
