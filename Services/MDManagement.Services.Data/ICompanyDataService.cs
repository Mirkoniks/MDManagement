namespace MDManagement.Services.Data
{
    using MDManagement.Services.Models.Company;

    public interface ICompanyDataService
    {
        public CompanyServiceModel FindById(int? id);

        public CompanyServiceModel FindByName(string name);

        public void Create(CreateCompanyServiceModel model);

        public bool Exists(string companyName);

        public bool IsValidCompany(string companyCode);

        public CompanyServiceModel FindByCompanyCode(string companyCode);

        public bool HasEmployees(int? companyId);

    }
}
